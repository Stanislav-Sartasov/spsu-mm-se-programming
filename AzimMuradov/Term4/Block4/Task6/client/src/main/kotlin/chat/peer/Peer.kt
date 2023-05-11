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
import java.net.InetSocketAddress
import java.net.ServerSocket
import java.net.Socket
import java.net.SocketException


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
            scope.launch { runPeerServer(peerServerPort) }
        } catch (e: SocketException) {
            logger.error(e.stackTraceToString())
            close()
        } finally {
            canCallRun = false
        }
    }

    fun connectToHub(hubIp: String, hubPort: Int) {
        Socket(hubIp, hubPort).use { client ->
            val reader = client.reader()
            val printer = client.printer()

            scope.launch {
                joinTheChatIntents.collect { intent ->
                    username = intent.username
                    printer.println(Json.encodeToString<P2HNews>(P2HNews.JoinTheChatRequest(user)))
                }
            }

            reader.forEachLine { text ->
                tryOrNull { Json.decodeFromString<H2PNews>(text) }?.let { news ->
                    when (news) {
                        is H2PNews.ChatInfo -> _state.update { st ->
                            when (st) {
                                is State.Chat -> st.copy(users = news.users)
                                State.Idle -> State.Chat(news.users, emptyList())
                            }
                        }

                        is H2PNews.Error -> throw LoginException(message = news.message)
                    }
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
                        .forEach { scope.launch { connectToPeer(it.address) } }
                    connectedUsers.update { state.users }
                }
            }.collect()
    }

    private fun connectToPeer(address: InetSocketAddress) {
        try {
            Socket(address.address, address.port).use { client ->
                client.reader().forEachLine { text ->
                    tryOrNull { Json.decodeFromString<P2PNews>(text) }?.let { news ->
                        when (news) {
                            is P2PNews.TextMessage -> _state.update { state ->
                                when (state) {
                                    is State.Chat -> {
                                        val sender = state.users.first { it.address == address }
                                        state.copy(messages = state.messages + (sender to news.msg))
                                    }

                                    State.Idle -> state
                                }
                            }
                        }
                    }
                }
            }
        } catch (e: SocketException) {
            logger.warn(e.stackTraceToString())
        }
    }

    private fun runPeerServer(peerServerPort: Int) {
        val peerServerSocket = ServerSocket(peerServerPort).also { peerServerSocket = it }

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
