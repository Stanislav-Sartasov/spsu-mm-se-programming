package chat.serializers

import kotlinx.serialization.KSerializer
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder
import java.net.InetAddress
import java.net.InetSocketAddress


// Composite serializer via surrogate:
// https://github.com/Kotlin/kotlinx.serialization/blob/master/docs/serializers.md#composite-serializer-via-surrogate

internal object InetSocketAddressSerializer : KSerializer<InetSocketAddress> {

    override val descriptor: SerialDescriptor = InetSocketAddressSurrogate.serializer().descriptor

    override fun serialize(encoder: Encoder, value: InetSocketAddress) {
        val surrogate = InetSocketAddressSurrogate(value.address.hostAddress, value.port)
        encoder.encodeSerializableValue(InetSocketAddressSurrogate.serializer(), surrogate)
    }

    override fun deserialize(decoder: Decoder): InetSocketAddress {
        val surrogate = decoder.decodeSerializableValue(InetSocketAddressSurrogate.serializer())
        return InetSocketAddress(InetAddress.getByName(surrogate.ip), surrogate.port)
    }
}

@Serializable
@SerialName("InetSocketAddress")
private data class InetSocketAddressSurrogate(val ip: String, val port: Int)
