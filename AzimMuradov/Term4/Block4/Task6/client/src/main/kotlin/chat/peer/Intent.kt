package chat.peer

import chat.data.Username
import java.time.Instant


sealed interface Intent {

    data class JoinTheChat(val username: Username) : Intent

    data class SendMessage(val text: String, val sendTime: Instant) : Intent
}
