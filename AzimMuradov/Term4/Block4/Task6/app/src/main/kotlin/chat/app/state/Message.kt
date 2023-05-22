package chat.app.state

import java.time.Instant


data class Message(
    val sender: User,
    val text: String,
    val sendTime: Instant,
)
