package chat.data

import kotlinx.serialization.Serializable


/**
 * Peer to peer news.
 */
@Serializable
sealed class P2PNews {

    @Serializable
    data class TextMessage(val msg: MessageData) : P2PNews()
}
