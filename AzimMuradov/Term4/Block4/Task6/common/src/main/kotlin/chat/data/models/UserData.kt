package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
data class UserData(
    val name: UserName,
    val ip: String,
    val port: Int,
)
