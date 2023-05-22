package chat.serializers

import kotlinx.serialization.KSerializer
import kotlinx.serialization.descriptors.SerialDescriptor
import kotlinx.serialization.descriptors.buildClassSerialDescriptor
import kotlinx.serialization.encoding.Decoder
import kotlinx.serialization.encoding.Encoder
import java.time.Instant


internal object InstantSerializer : KSerializer<Instant> {

    override val descriptor: SerialDescriptor = buildClassSerialDescriptor(serialName = "Instant") {}

    override fun serialize(encoder: Encoder, value: Instant) = encoder.encodeLong(value.epochSecond)

    override fun deserialize(decoder: Decoder): Instant = Instant.ofEpochSecond(decoder.decodeLong())
}
