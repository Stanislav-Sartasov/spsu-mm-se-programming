package chat

import chat.data.models.UserName


sealed interface Intent {

    data class JoinTheChat(val user: UserName) : Intent

    data class SendMessage(val text: String) : Intent

    object LeaveTheChat : Intent
}
