package chat

import chat.data.models.MessageData
import chat.data.models.UserData


sealed interface State {

    object Idle : State

    data class Chat(
        val users: List<UserData>,
        val messages: List<Pair<UserData, MessageData>>,
    ) : State
}
