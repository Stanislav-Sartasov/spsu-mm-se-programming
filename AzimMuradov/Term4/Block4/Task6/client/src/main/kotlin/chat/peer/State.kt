package chat.peer

import chat.data.MessageData
import chat.data.UserData


sealed interface State {

    object Idle : State

    data class Chat(
        val users: Set<UserData>,
        val messages: List<Pair<UserData, MessageData>>,
    ) : State
}
