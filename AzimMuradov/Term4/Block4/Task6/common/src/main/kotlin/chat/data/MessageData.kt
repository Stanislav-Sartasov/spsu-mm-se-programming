package chat.data

import chat.serializers.InstantSerializer
import kotlinx.serialization.Serializable
import java.time.Instant


@Serializable
data class MessageData(
    val text: String,
    @Serializable(with = InstantSerializer::class) val sendTime: Instant,
)
