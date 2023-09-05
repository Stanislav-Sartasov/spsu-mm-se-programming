package pipe.commands

import lib.pipe.commands.ExitCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertNull

class TestExit {
    @Test
    fun `Test exit`() {
        val output = ExitCommand("exit").run(emptyArray())
        assertNull(output.output)
        assertNull(output.error)
        assert(output.exit)
    }
}