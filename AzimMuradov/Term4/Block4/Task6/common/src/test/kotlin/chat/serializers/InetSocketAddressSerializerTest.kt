package chat.serializers

import kotlinx.serialization.json.Json
import java.net.InetAddress
import java.net.InetSocketAddress
import kotlin.test.Test
import kotlin.test.assertEquals


class InetSocketAddressSerializerTest {

    @Test
    fun `test serializer`() {
        val address = InetSocketAddress(InetAddress.getByName("127.0.0.1"), 1234)
        assertEquals(
            expected = """{"ip":"127.0.0.1","port":1234}""",
            actual = Json.encodeToString(InetSocketAddressSerializer, address)
        )
    }

    @Test
    fun `test deserializer`() {
        val address = InetSocketAddress(InetAddress.getByName("127.0.0.1"), 1234)
        assertEquals(
            expected = address,
            actual = Json.decodeFromString(InetSocketAddressSerializer, """{"ip":"127.0.0.1","port":1234}""")
        )
    }
}
