package connection

import lib.weather.connection.Connection
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestConnection {
    @Test
    fun `Test server arent exist`() {
        val connection = Connection("https://127.0.0.1.0")
        assertEquals(connection.requestGet(), "Unknow host")
        connection.disconect()
    }

    @Test
    //run "python3 -m http.server --bind 127.0.0.1 9000" in src/test/resources
    fun `Test get json`() {
        val connection = Connection("http://127.0.0.1:9000/test.json")
        connection.requestGet()
        val jo = connection.getResponseInJSON()
        assert(jo.has("success"))
        connection.disconect()
    }

    @Test
    fun `Test connection server with 401 error`() {
        val connection = Connection("https://api.tomorrow.io/v4/timelines")
        assertEquals(connection.requestGet(), "401")
        val jo = connection.getResponseInJSON()
        assert(jo.has("error"))
        connection.disconect()
    }
}
