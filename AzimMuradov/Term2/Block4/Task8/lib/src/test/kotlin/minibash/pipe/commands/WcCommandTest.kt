package minibash.pipe.commands

import minibash.TestUtils.asArguments
import minibash.TestUtils.startsWith
import minibash.TestUtils.times
import minibash.pipe.CommandRunOut
import org.junit.jupiter.api.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import java.io.File
import kotlin.io.path.createTempDirectory
import kotlin.test.*
import java.lang.System.lineSeparator as sep

internal class WcCommandTest {

    private lateinit var wc: WcCommand

    private lateinit var tempFolder: File

    private val tempFilePath get() = tempFolder.resolve(relative = "temp").absolutePath

    @BeforeEach
    fun setUp() {
        wc = WcCommand(name = "wc")
        tempFolder = File(createTempDirectory(prefix = "temp").toUri())
    }

    @AfterEach
    fun tearDown() {
        tempFolder.deleteRecursively()
    }


    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = WcCommand(name = "cmd").name)
    }

    @ParameterizedTest
    @MethodSource("inputsWithCommandRunOuts")
    fun `run command without arguments`(input: Sequence<Char>?, commandRunOut: CommandRunOut) {
        val (output, errors, signal) = wc.run(args = emptyList(), input = input)

        assertContentEquals(expected = commandRunOut.output, actual = output)
        assertContentEquals(expected = commandRunOut.errors, actual = errors)
        assertEquals(expected = commandRunOut.signal, actual = signal)
    }

    @ParameterizedTest
    @MethodSource("commonArgsForTests")
    fun `run command with one argument and various inputs`(
        fileText: String,
        output: Sequence<Char>,
        input: Sequence<Char>?,
    ) {
        File(tempFilePath).writeText(text = fileText)

        val (actualOutput, errors, signal) = wc.run(listOf(tempFilePath), input)

        assertContentEquals(expected = output, actual = actualOutput)
        assertNull(errors)
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("inputs")
    fun `run command and fail with one argument and various inputs`(input: Sequence<Char>?) {
        val (output, errors, signal) = wc.run(listOf("-".repeat(n = 1000000)), input)

        assertNull(output)
        assertNotNull(errors)
        assertTrue { errors.startsWith("wc: FileNotFoundException".asSequence()) }
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("commonArgsForTests")
    fun `run command with two argument and various inputs`(
        fileText: String,
        output: Sequence<Char>,
        input: Sequence<Char>?,
    ) {
        File(tempFilePath).writeText(text = fileText)

        val (actualOutput, errors, signal) = wc.run(listOf(tempFilePath, tempFilePath), input)

        assertContentEquals(expected = output + output, actual = actualOutput)
        assertNull(errors)
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("commonArgsForTests")
    fun `run command and partially fail with two argument and various inputs`(
        fileText: String,
        output: Sequence<Char>,
        input: Sequence<Char>?,
    ) {
        File(tempFilePath).writeText(text = fileText)

        val (actualOutput, errors, signal) = wc.run(listOf(tempFilePath, "-".repeat(n = 1000000)), input)

        assertContentEquals(expected = output, actual = actualOutput)
        assertNotNull(errors)
        assertTrue { errors.startsWith("wc: FileNotFoundException".asSequence()) }
        assertNull(signal)
    }


    private companion object {

        // Number of bytes in line separator

        val nl = sep().toByteArray().size


        @JvmStatic
        fun inputsWithCommandRunOuts() = listOf(
            null to CommandRunOut(errors = "wc: no arguments${sep()}".asSequence()),
            emptySequence<Char>() to CommandRunOut(output = " 0 0 0${sep()}".asSequence()),
            "abc".asSequence() to CommandRunOut(output = " 0 1 3${sep()}".asSequence()),
            "abc xyz".asSequence() to CommandRunOut(output = " 0 2 7${sep()}".asSequence()),
            "abc  xyz".asSequence() to CommandRunOut(output = " 0 2 8${sep()}".asSequence()),
            "abc${sep()}".asSequence() to CommandRunOut(output = " 1 1 ${3 + nl}${sep()}".asSequence()),
            "abc${sep()}xyz".asSequence() to CommandRunOut(output = " 1 2 ${6 + nl}${sep()}".asSequence()),
            "abc${sep()}xyz${sep()}12345".asSequence() to CommandRunOut(output = "   2   3  ${11 + nl * 2}${sep()}".asSequence())
        ).asArguments()

        @JvmStatic
        fun fileTextsWithOutputs() = listOf(
            "" to " 0 0 0${sep()}".asSequence(),
            "abc" to " 0 1 3${sep()}".asSequence(),
            "abc xyz" to " 0 2 7${sep()}".asSequence(),
            "abc  xyz" to " 0 2 8${sep()}".asSequence(),
            "abc${sep()}" to " 1 1 ${3 + nl}${sep()}".asSequence(),
            "abc${sep()}xyz" to " 1 2 ${6 + nl}${sep()}".asSequence(),
            "abc${sep()}xyz${sep()}12345" to "   2   3  ${11 + nl * 2}${sep()}".asSequence(),
        )

        @JvmStatic
        fun inputs() = listOf(
            null,
            "",
            "abc",
            "abc xyz",
            "abc  xyz",
            "abc${sep()}",
            "abc${sep()}xyz",
            "abc${sep()}xyz${sep()}12345"
        ).map { it?.asSequence() }

        @JvmStatic
        fun commonArgsForTests() = (fileTextsWithOutputs() * inputs()).map { (a, b) ->
            Triple(a.first, a.second, b)
        }.asArguments()
    }
}
