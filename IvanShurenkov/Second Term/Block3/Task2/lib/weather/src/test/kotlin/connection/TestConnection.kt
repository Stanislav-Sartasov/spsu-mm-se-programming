package connection

import io.mockk.every
import io.mockk.spyk
import lib.weather.connection.Connection
import org.junit.jupiter.api.Test
import java.io.File
import java.net.HttpURLConnection
import java.net.URL
import kotlin.test.assertEquals

class TestConnection {
    @Test
    fun `Test server arent exist`() {
        val url = "http://127.0.0.1:9000"
        val connection = Connection(url)
        connection.disconect()
        assertEquals(connection.requestGet().toIntOrNull(), null)
    }

    @Test
    fun `Test get normal json`() {
        val url = "http://127.0.0.1:9000/test.json"
        val connection = Connection(url)
        val conn = spyk(URL(url).openConnection() as HttpURLConnection)

        every { conn.responseCode } returns 200
        every { conn.inputStream } returns File("src/test/resources/testConnection1.json").inputStream()
        every { conn.disconnect() } answers { nothing }

        connection.setConnection(conn)
        connection.requestGet()

        val jo = connection.getResponseInJSON()
        assert(jo.has("success") && (jo.get("success") as Boolean))
        connection.disconect()
    }

    @Test
    fun `Test connection server with 401 error`() {
        val connection = Connection("https://api.tomorrow.io/v4/timelines")
        assertEquals(connection.requestGet(), "401")
        val jo = connection.getResponseInJSON()
        connection.disconect()
    }
}
