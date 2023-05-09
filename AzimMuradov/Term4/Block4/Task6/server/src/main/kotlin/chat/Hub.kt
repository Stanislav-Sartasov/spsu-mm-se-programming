package chat

import chat.data.models.Chat
import chat.data.models.ClientToServerMessage
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import java.io.InputStreamReader
import java.io.PrintWriter
import java.net.ServerSocket
import java.net.Socket
import chat.data.models.ClientToServerMessage as InMsg
import chat.data.models.ServerToClientMessage as OutMsg


class Hub(
    private val scope: CoroutineScope = CoroutineScope(Dispatchers.IO),
    private val state: MutableStateFlow<State> = MutableStateFlow(State(emptyMap())),
) {

    init {
        scope.launch {
            state.collect { logger.debug { it.users.entries.joinToString(separator = "\n") { it.value.userData.toString() } } }
        }
    }


    fun run(port: Int = 12345) {
        ServerSocket(port).use { server ->
            while (true) {
                val socket = server.accept()
                    .also { logger.debug { it } }
                scope.launch { connection(socket) }
            }
        }
    }


    private fun connection(socket: Socket) {
        socket.use {
            val reader = socket.reader()
            val printer = socket.printer()

            reader.forEachLine { text ->
                tryOrNull { Json.decodeFromString<InMsg>(text) }?.let { msg ->
                    when (msg) {
                        is ClientToServerMessage.JoinTheChat -> processJoinTheChat(msg, reader, printer)
                    }
                }
            }

            if (socket.isClosed) {
                logger.debug { true }
            }
        }
    }


    private fun processJoinTheChat(msg: InMsg.JoinTheChat, reader: InputStreamReader, printer: PrintWriter) {
        state.update { s ->
            val newUser = msg.user.name to FullUserData(
                userData = msg.user,
                reader = reader,
                printer = printer
            )

            if (msg.user.name !in s.users) {
                s.copy(users = s.users + newUser).also {
                    val newChatInfo = Json.encodeToString<OutMsg>(
                        OutMsg.ChatInfo(Chat(it.users.values.map(FullUserData::userData)))
                    )
                    it.users.values.forEach {
                        it.printer.println(
                            newChatInfo
                        )
                    }
                }
            } else {
                s.also {
                    printer.println(Json.encodeToString<OutMsg>(OutMsg.Error("")))
                }
            }
        }
    }
}
