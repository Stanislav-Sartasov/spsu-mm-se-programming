package pipe.commands

import lib.pipe.commands.EchoCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestEcho {
    @Test
    fun `Test echo command without args`() {
        val output = EchoCommand("echo").run(emptyArray())
        assertNull(output.error)
        assertEquals("\n", output.output)
        assert(!output.exit)
    }

    @Test
    fun `Test echo command with args`() {
        val output = EchoCommand("echo").run(arrayOf("a", "b"))
        assertNull(output.error)
        assertEquals("a b\n", output.output)
        assert(!output.exit)
    }
}