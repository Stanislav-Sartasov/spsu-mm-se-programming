package chat.data

import chat.serializers.InetSocketAddressSerializer
import kotlinx.serialization.Serializable
import java.net.InetSocketAddress


typealias Username = String

@Serializable
data class UserData(
    val username: Username,
    @Serializable(with = InetSocketAddressSerializer::class) val address: InetSocketAddress,
)
