package pipe.commands

import lib.pipe.commands.EchoCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestEcho {
    val sep = System.lineSeparator()
    @Test
    fun `Test echo command without args`() {
        val output = EchoCommand("echo").run(emptyArray())
        assertNull(output.error)
        assertEquals(sep, output.output)
        assert(!output.exit)
    }

    @Test
    fun `Test echo command with args`() {
        val output = EchoCommand("echo").run(arrayOf("a", "b"))
        assertNull(output.error)
        assertEquals("a b$sep", output.output)
        assert(!output.exit)
    }
}