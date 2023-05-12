package chat.hub

import chat.data.H2PNews
import chat.data.P2HNews
import chat.data.UserData
import chat.hub.state.Connection
import chat.hub.state.State
import chat.logger
import chat.printer
import chat.reader
import chat.tryOrNull
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.cancel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.flow.onEach
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.IOException
import java.io.PrintWriter
import java.net.ServerSocket
import java.net.Socket
import java.net.SocketAddress


class Hub(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
    private val state: MutableStateFlow<State> = MutableStateFlow(State(emptyList())),
) : AutoCloseable {

    private var serverSocket: ServerSocket? = null

    private var canCallRun: Boolean = true


    fun run(port: Int) {
        if (!canCallRun) return

        try {
            val serverSocket = ServerSocket(port).also { serverSocket = it }

            scope.launch { listenToStateChanges() }

            try {
                runServer(serverSocket)
            } catch (e: IOException) {
                logger.debug(e.stackTraceToString())
            }
        } finally {
            canCallRun = false
        }
    }

    override fun close() {
        canCallRun = false
        scope.cancel()
        serverSocket?.close()
    }


    private suspend fun listenToStateChanges() {
        state.onEach { state ->
            logger.debug {
                state.connections.joinToString(separator = "\n") { (user, address) -> "$user --- $address" }
            }
        }.onEach { state ->
            val newChatInfo = Json.encodeToString<H2PNews>(
                H2PNews.ChatInfo(users = state.connections.mapTo(mutableSetOf(), Connection::user))
            )
            state.connections.forEach { it.printer.println(newChatInfo) }
        }.collect()
    }

    private fun runServer(serverSocket: ServerSocket) {
        while (true) {
            val socket = serverSocket.accept().also { logger.debug { it } }

            scope.launch {
                try {
                    processPeerNews(socket)
                } catch (e: IOException) {
                    logger.warn(e.stackTraceToString())
                } finally {
                    val addr = socket.remoteSocketAddress
                    state.update { st ->
                        st.copy(connections = st.connections.filterNot { it.address == addr })
                    }
                }
            }
        }
    }

    private fun processPeerNews(socket: Socket) {
        socket.use {
            socket.reader().forEachLine { text ->
                logger.debug(text)

                when (val news = tryOrNull { Json.decodeFromString<P2HNews>(text) }) {
                    is P2HNews.JoinTheChatRequest -> processJoinTheChatRequest(
                        user = news.user,
                        address = socket.remoteSocketAddress,
                        printer = socket.printer()
                    )

                    null -> Unit
                }
            }
        }
    }

    private fun processJoinTheChatRequest(user: UserData, address: SocketAddress, printer: PrintWriter) {
        state.update { state ->
            if (state.connections.any { (u, a) -> u == user && a == address }) return

            val similarConnection = state.connections.find { (u, a) ->
                u.username == user.username || u.address == user.address || a == address
            }

            if (similarConnection == null) {
                state.copy(connections = state.connections + Connection(user, address, printer))
            } else {
                val errorMessage = when {
                    similarConnection.address == address -> "cannot change user data"
                    similarConnection.user.username == user.username -> "username is taken"
                    else -> "address is taken"
                }
                printer.println(Json.encodeToString<H2PNews>(H2PNews.LoginError(errorMessage)))
                return
            }
        }
    }
}
