package chat

import chat.data.models.MessageData
import chat.data.models.P2PMsg
import chat.data.models.UserData
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
import java.net.ServerSocket
import java.net.Socket
import java.time.Instant
import java.util.concurrent.atomic.AtomicBoolean
import kotlin.properties.Delegates
import chat.data.models.ClientToServerMessage as OutMsg
import chat.data.models.ServerToClientMessage as InMsg


class Peer(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
) {

    private lateinit var ss: ServerSocket

    private var port by Delegates.notNull<Int>()

    val state = MutableStateFlow<State>(State.Idle)

    val intents = MutableSharedFlow<Intent>()

    // private val jobs = ConcurrentHashMap<InetAddress, Job>()

    private val isAlive: AtomicBoolean = AtomicBoolean()

    private val connectedUsers = MutableStateFlow(listOf<UserData>())


    init {
        scope.launch {
            state.collect { s ->
                logger.debug { s }

                if (s is State.Chat) {
                    (s.users - connectedUsers.value).filter { it.port != port }.forEach {
                        launch { connectToClient(it.ip, it.port) }
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
        port = peerServerPort

        isAlive.set(true)

        ss = ServerSocket(peerServerPort)

        scope.launch { runPeerServer() }
        scope.launch { connectToHub(hubIp, hubPort) }
    }


    private suspend fun runPeerServer() {
        ss.use { server ->
            while (isAlive.get()) {
                val socket = server.accept()

                scope.launch {
                    socket.use { socket ->
                        val printer = socket.printer()

                        intents.collect {
                            if (it !is Intent.SendMessage) return@collect
                            printer.println(
                                Json.encodeToString<P2PMsg>(
                                    P2PMsg.TextMessage(
                                        MessageData(
                                            text = it.text,
                                            sendTimeInEpochSecond = Instant.now().epochSecond
                                        )
                                    )
                                )
                            )
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
                        is Intent.JoinTheChat -> printer.println(
                            Json.encodeToString<OutMsg>(
                                OutMsg.JoinTheChat(
                                    user = UserData(
                                        name = intent.user,
                                        ip = InetAddress.getLocalHost().hostAddress,
                                        port = this@Peer.port
                                    )
                                )
                            )
                        )

                        is Intent.SendMessage -> Unit

                        Intent.LeaveTheChat -> {
                            isAlive.set(false)
                            return@collect
                        }
                    }
                }
            }

            reader.forEachLine { text ->
                tryOrNull { Json.decodeFromString<InMsg>(text) }?.let { msg ->
                    when (msg) {
                        is InMsg.ChatInfo -> state.update { s ->
                            when (s) {
                                State.Idle -> State.Chat(msg.chat.users, emptyList())
                                is State.Chat -> s.copy(users = msg.chat.users)
                            }
                        }

                        is InMsg.Error -> error(msg.message)
                    }
                }
            }
        }
    }

    private fun connectToClient(ip: String, port: Int) {
        Socket(ip, port).use { client ->
            client.reader().forEachLine { text ->
                tryOrNull { Json.decodeFromString<P2PMsg>(text) }?.let { msg ->
                    when (msg) {
                        is P2PMsg.TextMessage -> state.update { s ->
                            logger.debug { s }
                            when (s) {
                                is State.Chat -> s.copy(
                                    messages = s.messages + (s.users.first { it.port == port } to msg.msg)
                                )

                                State.Idle -> error("")
                            }
                        }
                    }
                }
            }
        }
    }
}
