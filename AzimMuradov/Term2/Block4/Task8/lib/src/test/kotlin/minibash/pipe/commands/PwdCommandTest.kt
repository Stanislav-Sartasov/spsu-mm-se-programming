package minibash.pipe.commands

import minibash.TestUtils.asArguments
import minibash.TestUtils.times
import minibash.pipe.CommandRunOut
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import java.io.File
import java.nio.file.Path
import kotlin.test.*
import java.lang.System.lineSeparator as sep

internal class PwdCommandTest {

    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = PwdCommand(name = "cmd").name)
    }

    @ParameterizedTest
    @MethodSource("inputsWithCommandRunOuts")
    fun `run command without arguments`(input: Sequence<Char>?, commandRunOut: CommandRunOut) {
        val pwd = PwdCommand(name = "pwd")
        val (output, errors, signal) = pwd.run(emptyList(), input)

        assertContentEquals(expected = commandRunOut.output, actual = output)
        assertContentEquals(expected = commandRunOut.errors, actual = errors)
        assertEquals(expected = commandRunOut.signal, actual = signal)
    }

    @ParameterizedTest
    @MethodSource("argumentsWithInputs")
    fun `run command and fail with non-zero arguments and various inputs`(
        args: List<String>,
        input: Sequence<Char>?,
    ) {
        val pwd = PwdCommand(name = "pwd")
        val (output, errors, signal) = pwd.run(args, input)

        assertNull(output)
        assertContentEquals(expected = "pwd: too many arguments${sep()}".asSequence(), actual = errors)
        assertNull(signal)
    }


    private companion object {

        val pwdOut = run {
            val wd = Path.of("").toRealPath().toString()
            val ls = File(wd).listFiles()
                ?.sortedWith(Comparator.comparing<File, Boolean> { !it.isDirectory }.thenComparing<String> { it.name })
                ?.joinToString(separator = sep()) {
                    if (it.isDirectory) "${it.name}${File.separator}" else it.name
                } ?: ""
            CommandRunOut(output = "$wd${sep()}${sep()}$ls${sep()}".asSequence())
        }

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
        fun inputsWithCommandRunOuts() = (inputs() * listOf(pwdOut)).asArguments()

        @JvmStatic
        fun argumentsWithInputs() = (arguments() * inputs()).asArguments()
    }
}
