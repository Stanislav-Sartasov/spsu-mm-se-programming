package connection

import TestObject
import io.mockk.every
import io.mockk.mockk
import io.mockk.verify
import lib.weather.connection.Connection
import lib.weather.stream.*
import org.junit.jupiter.api.Test
import org.kodein.di.DI
import org.kodein.di.bindSingleton
import org.kodein.di.instance
import java.io.File
import java.net.HttpURLConnection
import kotlin.test.assertEquals

class TestConnection {
    val stream = DI {
        bindSingleton<Stream>() { Stream() }
    }

    @Test
    fun `Test server arent exist`() {
        val url = "http://127.0.0.1:9000"
        val connection = Connection(TestObject.getURLConnection(url), stream)
        connection.disconect()
        assertEquals(connection.requestGet().toIntOrNull(), null)
    }

    @Test
    fun `Test get normal json`() {
        val conn = mockk<HttpURLConnection>()
        // (URL(url).openConnection() as HttpURLConnection)

        every { conn.setRequestProperty(any(), any()) } answers  { callOriginal() }
        every { conn.requestMethod = any() } answers { callOriginal() }
        every { conn.responseCode } returns 200
        every { conn.inputStream } returns File("src/test/resources/testConnection1.json").inputStream()
        every { conn.disconnect() } answers { nothing }

        val connection = Connection(conn, stream)
        connection.requestGet()

        val jo = connection.getResponseInJSON()
        assert(jo.has("success") && (jo.get("success") as Boolean))
        connection.disconect()
    }

    @Test
    fun `Test error code in requestGet`() {
        val responseCode = 5000
        val conn = mockk<HttpURLConnection>()

        every { conn.setRequestProperty(any(), any()) } answers  { callOriginal() }
        every { conn.requestMethod = any() } answers { callOriginal() }
        every { conn.responseCode } returns responseCode
        every { conn.inputStream } returns File("src/test/resources/testConnection1.json").inputStream()
        every { conn.disconnect() } answers { nothing }

        val testStream = DI {
            bindSingleton<Stream>() { mockk<Stream>() }
        }

        val err: Stream by testStream.instance()
        every { err.printErr(any()) } answers { nothing }

        val connection = Connection(conn, testStream)
        assertEquals("$responseCode", connection.requestGet())

        val json = connection.getResponseInJSON()
        assertEquals("{\"error\":\"$responseCode\"}", json.toString())
    }

    @Test
    fun `Test setAuthorizationHeader`() {
        val conn = mockk<HttpURLConnection>()
        val api = "api"

        every { conn.setRequestProperty(any(), any()) } answers  { nothing }

        val connection = Connection(conn, stream)
        connection.setAuthorizationHeader(api)

        verify { conn.setRequestProperty("Authorization", api) }
    }

    @Test
    fun `Test error convert to Json`() {
        val conn = mockk<HttpURLConnection>()

        every { conn.setRequestProperty(any(), any()) } answers  { callOriginal() }
        every { conn.requestMethod = any() } answers { callOriginal() }
        every { conn.responseCode } returns 200
        every { conn.inputStream } returns File("src/test/resources/testConnection2.json").inputStream()
        every { conn.disconnect() } answers { nothing }

        val connection = Connection(conn, stream)
        val json = connection.getResponseInJSON()
        assertEquals("{\"error\":\"0\"}", json.toString())
    }
}
