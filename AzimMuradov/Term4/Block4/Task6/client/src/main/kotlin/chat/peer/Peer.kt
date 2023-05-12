package chat.peer

import chat.data.*
import chat.logger
import chat.printer
import chat.reader
import chat.tryOrNull
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.cancel
import kotlinx.coroutines.flow.*
import kotlinx.coroutines.launch
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.IOException
import java.net.InetSocketAddress
import java.net.ServerSocket
import java.net.Socket


class Peer(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
    private val _state: MutableStateFlow<State> = MutableStateFlow(State.Idle),
    val intents: MutableSharedFlow<Intent> = MutableSharedFlow(),
) : AutoCloseable {

    val state: StateFlow<State> get() = _state

    private val joinTheChatIntents: SharedFlow<Intent.JoinTheChat> = intents.filterIsInstance<Intent.JoinTheChat>()
        .shareIn(scope, SharingStarted.Lazily)

    private val sendMessageIntents: SharedFlow<Intent.SendMessage> = intents.filterIsInstance<Intent.SendMessage>()
        .shareIn(scope, SharingStarted.Lazily)


    val user: UserData get() = UserData(username, address)

    lateinit var username: Username
        private set

    lateinit var address: InetSocketAddress
        private set


    private var peerServerSocket: ServerSocket? = null

    private val connectedUsers = MutableStateFlow(setOf<UserData>())

    private var canCallRun: Boolean = true


    @JvmOverloads
    fun run(peerServerPort: Int = 0) {
        if (!canCallRun) return

        try {
            scope.launch { listenToStateChanges() }

            peerServerSocket = ServerSocket(peerServerPort)

            scope.launch {
                try {
                    runPeerServer()
                } catch (e: IOException) {
                    logger.debug(e.stackTraceToString())
                }
            }
        } finally {
            canCallRun = false
        }
    }

    fun connectToHub(hubAddress: String, hubPort: Int) {
        Socket(hubAddress, hubPort).use { client ->
            val reader = client.reader()
            val printer = client.printer()

            scope.launch {
                joinTheChatIntents.collect { intent ->
                    username = intent.username
                    printer.println(Json.encodeToString<P2HNews>(P2HNews.JoinTheChatRequest(user)))
                }
            }

            reader.forEachLine { text ->
                when (val news = tryOrNull { Json.decodeFromString<H2PNews>(text) }) {
                    is H2PNews.ChatInfo -> _state.update { state ->
                        when (state) {
                            is State.Chat -> state.copy(users = news.users)
                            State.Idle -> State.Chat(users = news.users, messages = emptyList())
                        }
                    }

                    is H2PNews.LoginError -> throw LoginException(message = news.message)

                    null -> Unit
                }
            }
        }
    }

    override fun close() {
        canCallRun = false
        scope.cancel()
        peerServerSocket?.close()
    }


    private suspend fun listenToStateChanges() {
        state.onEach { logger.debug { it } }
            .onEach { state ->
                if (state is State.Chat) {
                    (state.users - connectedUsers.value)
                        .filter { it.username != username }
                        .forEach {
                            scope.launch {
                                try {
                                    connectToPeer(it.address)
                                } catch (e: IOException) {
                                    logger.warn(e.stackTraceToString())
                                }
                            }
                        }
                    connectedUsers.update { state.users }
                }
            }.collect()
    }

    private fun connectToPeer(address: InetSocketAddress) {
        Socket(address.address, address.port).use { client ->
            client.reader().forEachLine { text ->
                when (val news = tryOrNull { Json.decodeFromString<P2PNews>(text) }) {
                    is P2PNews.TextMessage -> _state.update { state ->
                        when (state) {
                            is State.Chat -> {
                                val sender = state.users.first { it.address == address }
                                state.copy(messages = state.messages + (sender to news.msg))
                            }

                            State.Idle -> state
                        }
                    }

                    null -> Unit
                }
            }
        }
    }

    private fun runPeerServer() {
        val peerServerSocket = peerServerSocket!!

        address = InetSocketAddress(peerServerSocket.inetAddress, peerServerSocket.localPort)

        while (true) {
            val socket = peerServerSocket.accept()

            scope.launch {
                socket.use { socket ->
                    val printer = socket.printer()

                    sendMessageIntents.collect {
                        val msg = MessageData(it.text, it.sendTime)

                        _state.update { state ->
                            when (state) {
                                is State.Chat -> state.copy(messages = state.messages + (user to msg))
                                State.Idle -> state
                            }
                        }

                        printer.println(Json.encodeToString<P2PNews>(P2PNews.TextMessage(msg)))
                    }
                }
            }
        }
    }
}
