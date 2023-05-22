package ui.mvi

import domain.ChatServerInterface
import domain.Message
import domain.User
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.consumeAsFlow
import kotlinx.coroutines.flow.runningFold
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import net.utils.combineStates
import org.koin.core.component.KoinComponent
import org.koin.core.component.inject

class ChatStore(
    scope: CoroutineScope,
) : AbstractStore<ChatIntent, ChatState>(scope), KoinComponent {
    private val p2pServer: ChatServerInterface by inject()

    override val stateFlow = combineStates(
        flow1 = intentChannel.consumeAsFlow()
            .runningFold(ChatState.Disconnected, ::fold)
            .stateIn(scope, SharingStarted.Eagerly, ChatState.Disconnected),
        flow2 = p2pServer.connectedUsers,
        transform = { chatState, knownUsers ->
            when (chatState) {
                is ChatState.Connected -> chatState.copy(knownUsers = knownUsers)
                else -> chatState
            }
        }
    )

    init {
        scope.launch {
            p2pServer.receivedMessages.collect { send(ChatIntent.ReceiveMessage(it)) }
        }
    }

    override suspend fun fold(curState: ChatState, intent: ChatIntent): ChatState {
        return when (intent) {
            is ChatIntent.Start -> start(intent.userName, intent.port)
            is ChatIntent.Connect -> connect(intent)
            is ChatIntent.SendMessage -> sendMessage(intent.message)
            is ChatIntent.ReceiveMessage -> receiveMessage(intent.message)
            is ChatIntent.OnError -> (curState as ChatState.Error).previousState
            is ChatIntent.OnClose -> {
                p2pServer.close()
                ChatState.Closed
            }
        }
    }

    private fun start(username: String, port: Int): ChatState {
        val result = p2pServer.start(user = User(name = username), port = port)
        return if (result.isFailure) {
            ChatState.Error(error = result.exceptionOrNull()!!, previousState = stateFlow.value)
        } else {
            ChatState.Connected(username, port)
        }
    }

    private suspend fun connect(conn: ChatIntent.Connect): ChatState {
        val state = stateFlow.value
        val result = p2pServer.connect(conn.ip, conn.port)
        return if (result.isSuccess) {
            when (state) {
                is ChatState.Connected -> state
                else -> error("")
            }
        } else {
            ChatState.Error(result.exceptionOrNull()!!, state)
        }
    }

    private suspend fun sendMessage(message: String): ChatState {
        return when (val state = stateFlow.value) {
            is ChatState.Connected -> {
                val msg = Message.Normal(from = state.username, msg = message)
                p2pServer.broadcastMessage(msg)
                state
            }

            else -> error("")
        }
    }

    private fun receiveMessage(message: Message): ChatState {
        val state = stateFlow.value
        return when (message) {
            is Message.Normal -> when (state) {
                is ChatState.Connected -> state.copy(messages = state.messages + message)
                else -> state
            }

            else -> state
        }
    }
}

fun CoroutineScope.createChatStore(): ChatStore {
    return ChatStore(this)
}


