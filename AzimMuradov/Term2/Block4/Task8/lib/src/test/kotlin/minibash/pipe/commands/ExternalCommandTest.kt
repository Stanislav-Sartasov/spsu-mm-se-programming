package minibash.pipe.commands

import minibash.TestUtils.asArguments
import minibash.TestUtils.startsWith
import minibash.TestUtils.times
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.*
import java.lang.System.lineSeparator as sep

internal class ExternalCommandTest {

    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = ExternalCommand(name = "cmd").name)
    }

    @ParameterizedTest
    @MethodSource("argumentsWithInputs")
    fun `fail to run non-existing command`(args: List<String>, input: Sequence<Char>?) {
        val name = "-".repeat(n = 10000)

        val cmd = ExternalCommand(name)
        val (output, errors, signal) = cmd.run(args, input)

        assertNull(output)
        assertNotNull(errors)
        assertTrue { errors.startsWith("$name: IOException".asSequence()) }
        assertNull(signal)
    }


    private companion object {

        @JvmStatic
        fun inputs() = listOf(
            null,
            "",
            "abc",
            "abc xyz",
            "abc${sep()}",
            "abc${sep()}xyz",
        ).map { it?.asSequence() }

        @JvmStatic
        fun arguments() = listOf(
            listOf(""),
            listOf("abc"),
            listOf("abc", ""),
            listOf("abc", "xyz"),
            listOf("abc", "xyz", "123"),
        )

        @JvmStatic
        fun argumentsWithInputs() = (arguments() * inputs()).asArguments()
    }
}
