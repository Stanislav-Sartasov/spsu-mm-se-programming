package chat.data.models

import kotlinx.serialization.Serializable


@Serializable
sealed class P2PMsg {

    @Serializable
    data class TextMessage(val msg: MessageData) : P2PMsg()
}
