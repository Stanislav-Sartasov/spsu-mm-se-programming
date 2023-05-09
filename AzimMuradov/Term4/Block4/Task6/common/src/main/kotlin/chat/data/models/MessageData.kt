package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
data class MessageData(
    val text: String,
    val sendTimeInEpochSecond: Long,
)
