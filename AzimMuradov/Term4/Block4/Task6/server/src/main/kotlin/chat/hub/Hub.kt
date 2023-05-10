package chat.hub

import chat.data.H2PNews
import chat.data.P2HNews
import chat.logger
import chat.printer
import chat.reader
import chat.tryOrNull
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.PrintWriter
import java.net.ServerSocket
import java.net.Socket


class Hub(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
    private val state: MutableStateFlow<State> = MutableStateFlow(State(emptyMap())),
) {

    init {
        scope.launch {
            state.collect {
                logger.debug {
                    it.users.values.joinToString(separator = "\n") { (user) -> user.toString() }
                }
            }
        }
    }

    fun run(port: Int) {
        ServerSocket(port).use { server ->
            while (true) {
                val socket = server.accept().also { logger.debug { it } }
                scope.launch { establishConnection(socket) }
            }
        }
    }


    private fun establishConnection(socket: Socket) {
        socket.use {
            socket.reader().forEachLine { text ->
                tryOrNull { Json.decodeFromString<P2HNews>(text) }?.let { news ->
                    when (news) {
                        is P2HNews.JoinTheChatRequest -> processJoinTheChatRequest(news, socket.printer())
                    }
                }
            }

            if (socket.isClosed) {
                logger.debug { true }
            }
        }
    }

    private fun processJoinTheChatRequest(news: P2HNews.JoinTheChatRequest, printer: PrintWriter) {
        val username = news.user.username
        val data = news.user to printer

        state.update { st ->
            when {
                st.users[username] == data -> st

                username in st.users -> st.also {
                    printer.println(
                        Json.encodeToString<H2PNews>(H2PNews.Error(reason = "user already exists"))
                    )
                }

                else -> st.copy(users = st.users + (username to data)).also {
                    val newChatInfo = Json.encodeToString<H2PNews>(
                        H2PNews.ChatInfo(users = it.users.values.mapTo(mutableSetOf()) { (user) -> user })
                    )
                    it.users.values.forEach { (_, printer) ->
                        printer.println(newChatInfo)
                    }
                }
            }
        }
    }
}
