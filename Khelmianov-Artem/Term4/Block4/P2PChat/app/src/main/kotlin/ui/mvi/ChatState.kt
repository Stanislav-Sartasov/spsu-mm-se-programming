package ui.mvi

import domain.Message
import domain.User


sealed class ChatState {
    object Disconnected : ChatState()
    object Closed : ChatState()

    data class Connected(
        val username: String,
        val port: Int,
        val knownUsers: List<User> = emptyList(),
        val messages: List<Message.Normal> = emptyList()
    ) : ChatState()

    data class Error(
        val error: Throwable,
        val previousState: ChatState
    ) : ChatState()
}
