package chat.peer

import chat.data.*
import chat.logger
import chat.printer
import chat.reader
import chat.tryOrNull
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.net.InetAddress
import java.net.InetSocketAddress
import java.net.ServerSocket
import java.net.Socket
import java.util.concurrent.atomic.AtomicBoolean


class Peer(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
) {

    val user: UserData get() = UserData(username, address)

    lateinit var username: Username
        private set

    lateinit var address: InetSocketAddress
        private set


    val state = MutableStateFlow<State>(State.Idle)

    val intents = MutableSharedFlow<Intent>()


    private val isAlive: AtomicBoolean = AtomicBoolean()

    private val connectedUsers = MutableStateFlow(setOf<UserData>())


    init {
        scope.launch {
            state.collect { s ->
                logger.debug { s }

                if (s is State.Chat) {
                    (s.users - connectedUsers.value).filter { it.username != username }.forEach {
                        launch { connectToPeer(it.address) }
                    }
                    connectedUsers.update { s.users }
                }
            }
        }
    }

    fun run(
        hubIp: String, hubPort: Int,
        peerServerPort: Int,
    ) {
        address = InetSocketAddress(InetAddress.getLocalHost(), peerServerPort)

        isAlive.set(true)

        scope.launch { runPeerServer(peerServerPort) }
        scope.launch { connectToHub(hubIp, hubPort) }
    }


    private suspend fun runPeerServer(peerServerPort: Int) {
        ServerSocket(peerServerPort).use { server ->
            while (isAlive.get()) {
                val socket = server.accept()

                scope.launch {
                    socket.use { socket ->
                        val printer = socket.printer()

                        intents.collect {
                            when (it) {
                                is Intent.SendMessage -> {
                                    val msg = MessageData(it.text, it.sendTime)

                                    state.update { st ->
                                        logger.debug { st }
                                        when (st) {
                                            is State.Chat -> st.copy(messages = st.messages + (user to msg))
                                            State.Idle -> st
                                        }
                                    }

                                    printer.println(Json.encodeToString<P2PNews>(P2PNews.TextMessage(msg)))
                                }

                                else -> return@collect
                            }
                        }
                    }
                }
            }
        }
    }

    private suspend fun connectToHub(ip: String, port: Int) {
        Socket(ip, port).use { client ->
            val reader = client.reader()
            val printer = client.printer()

            scope.launch {
                intents.collect { intent ->
                    when (intent) {
                        is Intent.JoinTheChat -> {
                            username = intent.username
                            printer.println(Json.encodeToString<P2HNews>(P2HNews.JoinTheChatRequest(user)))
                        }

                        is Intent.SendMessage -> Unit

                        Intent.LeaveTheChat -> {
                            isAlive.set(false)
                            return@collect
                        }
                    }
                }
            }

            reader.forEachLine { text ->
                tryOrNull { Json.decodeFromString<H2PNews>(text) }?.let { news ->
                    when (news) {
                        is H2PNews.ChatInfo -> state.update { st ->
                            when (st) {
                                is State.Chat -> st.copy(users = news.users)
                                State.Idle -> State.Chat(news.users, emptyList())
                            }
                        }

                        is H2PNews.Error -> error(news.reason)
                    }
                }
            }
        }
    }

    private fun connectToPeer(address: InetSocketAddress) {
        Socket(address.address, address.port).use { client ->
            client.reader().forEachLine { text ->
                tryOrNull { Json.decodeFromString<P2PNews>(text) }?.let { news ->
                    when (news) {
                        is P2PNews.TextMessage -> state.update { st ->
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
