package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
sealed class ServerToClientMessage {

    @Serializable
    data class ChatInfo(val chat: Chat) : ServerToClientMessage()

    @Serializable
    data class Error(val message: String) : ServerToClientMessage()
}
