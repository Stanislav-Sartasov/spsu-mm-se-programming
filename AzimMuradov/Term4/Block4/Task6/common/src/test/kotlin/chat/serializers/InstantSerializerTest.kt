package chat.serializers

import kotlinx.serialization.json.Json
import java.time.Instant
import kotlin.test.Test
import kotlin.test.assertEquals


class InstantSerializerTest {

    @Test
    fun `test serializer`() {
        val epochSecond: Long = 100000000
        assertEquals(
            expected = "$epochSecond",
            actual = Json.encodeToString(InstantSerializer, Instant.ofEpochSecond(epochSecond))
        )
    }

    @Test
    fun `test deserializer`() {
        val epochSecond: Long = 100000000
        assertEquals(
            expected = Instant.ofEpochSecond(epochSecond),
            actual = Json.decodeFromString(InstantSerializer, "$epochSecond")
        )
    }
}
