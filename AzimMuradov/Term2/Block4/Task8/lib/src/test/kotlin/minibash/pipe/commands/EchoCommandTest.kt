package minibash.pipe.commands

import minibash.TestUtils.asArguments
import minibash.pipe.CommandRunOut
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import java.lang.System.lineSeparator as sep

internal class EchoCommandTest {

    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = EchoCommand(name = "cmd").name)
    }

    @ParameterizedTest
    @MethodSource("argsForRunCommandTest")
    fun `run command`(args: List<String>, input: Sequence<Char>?, commandRunOut: CommandRunOut) {
        val echo = EchoCommand(name = "echo")
        val (output, errors, signal) = echo.run(args, input)

        assertContentEquals(expected = commandRunOut.output, actual = output)
        assertContentEquals(expected = commandRunOut.errors, actual = errors)
        assertEquals(expected = commandRunOut.signal, actual = signal)
    }


    private companion object {

        @JvmStatic
        fun argsForRunCommandTest() = listOf(
            Triple(
                first = emptyList(),
                second = null,
                third = CommandRunOut(output = sep().asSequence())
            ),
            Triple(
                first = listOf("abc"),
                second = null,
                third = CommandRunOut(output = "abc${sep()}".asSequence())
            ),
            Triple(
                first = listOf("abc", "xyz"),
                second = null,
                third = CommandRunOut(output = "abc xyz${sep()}".asSequence())
            ),
            Triple(
                first = listOf("abc", "xyz", ""),
                second = null,
                third = CommandRunOut(output = "abc xyz ${sep()}".asSequence())
            ),
            Triple(
                first = emptyList(),
                second = sequenceOf('a'),
                third = CommandRunOut(output = sep().asSequence())
            ),
            Triple(
                first = listOf("abc"),
                second = sequenceOf('a'),
                third = CommandRunOut(output = "abc${sep()}".asSequence())
            ),
            Triple(
                first = listOf("abc", "xyz"),
                second = sequenceOf('a'),
                third = CommandRunOut(output = "abc xyz${sep()}".asSequence())
            ),
            Triple(
                first = listOf("abc", "xyz", ""),
                second = sequenceOf('a'),
                third = CommandRunOut(output = "abc xyz ${sep()}".asSequence())
            ),
        ).asArguments()
    }
}
