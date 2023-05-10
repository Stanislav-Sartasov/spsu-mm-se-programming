package chat.data

import kotlinx.serialization.Serializable


/**
 * Peer to hub news.
 */
@Serializable
sealed class P2HNews {

    @Serializable
    data class JoinTheChatRequest(val user: UserData) : P2HNews()
}
