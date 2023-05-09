package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
data class Chat(
    val users: List<UserData>,
)
