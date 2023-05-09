package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
sealed class ClientToServerMessage {

    @Serializable
    data class JoinTheChat(val user: UserData) : ClientToServerMessage()

    // @Serializable
    // data class LeaveTheChat(val user: UserData) : ClientToServerMessage()
}
