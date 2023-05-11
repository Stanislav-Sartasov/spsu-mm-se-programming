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


    @JvmOverloads
    fun run(hubIp: String, hubPort: Int, peerServerPort: Int = 0) {
        scope.launch {
            state.onEach { logger.debug { it } }
                .onEach { st ->
                    if (st is State.Chat) {
                        (st.users - connectedUsers.value)
                            .filter { it.username != username }
                            .forEach { launch { connectToPeer(it.address) } }
                        connectedUsers.update { st.users }
                    }
                }.collect()
        }
        scope.launch { runPeerServer(peerServerPort) }
        scope.launch { connectToHub(hubIp, hubPort) }
    }

    override fun close() {
        scope.cancel()
        peerServerSocket?.close()
    }


    private fun runPeerServer(peerServerPort: Int) {
        val peerServerSocket = ServerSocket(peerServerPort).also { peerServerSocket = it }

        try {
            address = InetSocketAddress(peerServerSocket.inetAddress, peerServerSocket.localPort)

            while (true) {
                val socket = peerServerSocket.accept()

                scope.launch {
                    socket.use { socket ->
                        val printer = socket.printer()

                        sendMessageIntents.collect {
                            val msg = MessageData(it.text, it.sendTime)

                            _state.update { st ->
                                logger.debug { st }
                                when (st) {
                                    is State.Chat -> st.copy(messages = st.messages + (user to msg))
                                    State.Idle -> st
                                }
                            }

                            printer.println(Json.encodeToString<P2PNews>(P2PNews.TextMessage(msg)))
                        }
                    }
                }
            }
        } catch (e: SocketException) {
            logger.debug(e.stackTraceToString())
        }
    }

    private fun connectToHub(ip: String, port: Int) {
        try {
            Socket(ip, port).use { client ->
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

                            is H2PNews.Error -> error(news.message)
                        }
                    }
                }
            }
        } catch (e: SocketException) {
            logger.debug(e.stackTraceToString())
        }
    }

    private fun connectToPeer(address: InetSocketAddress) {
        Socket(address.address, address.port).use { client ->
            client.reader().forEachLine { text ->
                tryOrNull { Json.decodeFromString<P2PNews>(text) }?.let { news ->
                    when (news) {
                        is P2PNews.TextMessage -> _state.update { st ->
                            logger.debug { st }

                            when (st) {
                                is State.Chat -> {
                                    val sender = st.users.first { it.address == address }
                                    st.copy(messages = st.messages + (sender to news.msg))
                                }

                                State.Idle -> st
                            }
                        }
                    }
                }
            }
        }
    }
}
