package minibash.pipe.commands

import minibash.pipe.CommandRunOut
import minibash.pipe.Signal
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class ExitCommandTest {

    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = ExitCommand(name = "cmd").name)
    }

    @Test
    fun `run command`() {
        assertEquals(
            expected = CommandRunOut(signal = Signal.SIGINT),
            actual = ExitCommand(name = "exit").run(args = emptyList())
        )
    }
}
