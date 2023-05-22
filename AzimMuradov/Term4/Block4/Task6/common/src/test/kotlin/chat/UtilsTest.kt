package chat

import java.net.ServerSocket
import java.net.Socket
import kotlin.concurrent.thread
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotNull
import kotlin.test.assertNull


class UtilsTest {

    @Test
    fun `try to run code and get not null value`() {
        assertNotNull(tryOrNull { 17 })
    }

    @Test
    fun `try to run code and get null value due to exception`() {
        assertNull(tryOrNull<Int> { error("") })
    }

    @Test
    fun `test socket reader`() {
        val c: Char
        ServerSocket(5000).use {
            Socket("localhost", 5000).use {
                it.getOutputStream().write('a'.code)
            }
            it.accept().use { socket ->
                c = socket.reader().read().toChar()
            }
        }
        assertEquals(expected = 'a', actual = c)
    }

    @Test
    fun `test socket printer`() {
        var c: Char? = null
        ServerSocket(5000).use {
            val thread = thread {
                Socket("localhost", 5000).use { socket ->
                    c = socket.getInputStream().read().toChar()
                }
            }
            it.accept().use { socket ->
                socket.printer().println('a')
            }
            thread.join()
        }
        assertEquals(expected = 'a', actual = c)
    }
}
