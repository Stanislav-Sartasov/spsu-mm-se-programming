package chat.serializers

import kotlinx.serialization.KSerializer
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.descriptors.buildClassSerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder
import java.net.InetAddress
import java.net.InetSocketAddress


internal object InetSocketAddressSerializer : KSerializer<InetSocketAddress> {

    override val descriptor: SerialDescriptor = buildClassSerialDescriptor(serialName = "InetSocketAddress") {}

    override fun serialize(encoder: Encoder, value: InetSocketAddress) {
        encoder.encodeString(value.address.hostAddress)
        encoder.encodeInt(value.port)
    }

    override fun deserialize(decoder: Decoder): InetSocketAddress = InetSocketAddress(
        InetAddress.getByName(decoder.decodeString()),
        decoder.decodeInt()
    )
}
