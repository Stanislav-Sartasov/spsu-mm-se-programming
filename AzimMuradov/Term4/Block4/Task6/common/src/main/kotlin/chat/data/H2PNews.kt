package chat.data

import kotlinx.serialization.Serializable


/**
 * Hub to peer news.
 */
@Serializable
sealed class H2PNews {

    @Serializable
    data class ChatInfo(val users: Set<UserData>) : H2PNews()

    @Serializable
    data class LoginError(val message: String) : H2PNews()
}
