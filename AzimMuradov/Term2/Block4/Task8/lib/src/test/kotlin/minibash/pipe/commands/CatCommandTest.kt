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

internal class CatCommandTest {

    private lateinit var cat: CatCommand

    private lateinit var tempFolder: File

    private val tempFilePath get() = tempFolder.resolve(relative = "temp").absolutePath

    @BeforeEach
    fun setUp() {
        cat = CatCommand(name = "cat")
        tempFolder = File(createTempDirectory(prefix = "temp").toUri())
    }

    @AfterEach
    fun tearDown() {
        tempFolder.deleteRecursively()
    }


    @Test
    fun `get command name`() {
        assertEquals(expected = "cmd", actual = CatCommand(name = "cmd").name)
    }

    @ParameterizedTest
    @MethodSource("inputsWithCommandRunOuts")
    fun `run command without arguments`(input: Sequence<Char>?, commandRunOut: CommandRunOut) {
        val (output, errors, signal) = cat.run(args = emptyList(), input = input)

        assertContentEquals(expected = commandRunOut.output, actual = output)
        assertContentEquals(expected = commandRunOut.errors, actual = errors)
        assertEquals(expected = commandRunOut.signal, actual = signal)
    }

    @ParameterizedTest
    @MethodSource("fileTextsWithInputs")
    fun `run command with one argument and various inputs`(fileText: String, input: Sequence<Char>?) {
        File(tempFilePath).writeText(text = fileText)

        val (output, errors, signal) = cat.run(args = listOf(tempFilePath), input = input)

        assertContentEquals(expected = fileText.asSequence(), actual = output)
        assertNull(errors)
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("inputs")
    fun `run command and fail with one argument and various inputs`(input: Sequence<Char>?) {
        val (output, errors, signal) = cat.run(listOf("-".repeat(n = 1000000)), input)

        assertNull(output)
        assertNotNull(errors)
        assertTrue { errors.startsWith("cat: FileNotFoundException".asSequence()) }
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("fileTextsWithInputs")
    fun `run command with two argument and various inputs`(fileText: String, input: Sequence<Char>?) {
        File(tempFilePath).writeText(text = fileText)

        val (output, errors, signal) = cat.run(listOf(tempFilePath, tempFilePath), input)

        assertContentEquals(expected = fileText.repeat(n = 2).asSequence(), actual = output)
        assertNull(errors)
        assertNull(signal)
    }

    @ParameterizedTest
    @MethodSource("fileTextsWithInputs")
    fun `run command and partially fail with two argument and various inputs`(
        fileText: String,
        input: Sequence<Char>?,
    ) {
        File(tempFilePath).writeText(text = fileText)

        val (output, errors, signal) = cat.run(listOf(tempFilePath, "-".repeat(n = 1000000)), input)

        assertContentEquals(expected = fileText.asSequence(), actual = output)
        assertNotNull(errors)
        assertTrue { errors.startsWith("cat: FileNotFoundException".asSequence()) }
        assertNull(signal)
    }


    private companion object {

        @JvmStatic
        fun inputsWithCommandRunOuts() = listOf(
            null to CommandRunOut(errors = "cat: no arguments${sep()}".asSequence()),
            emptySequence<Char>() to CommandRunOut(output = emptySequence()),
            "abc".asSequence() to CommandRunOut(output = "abc".asSequence()),
            "abc xyz".asSequence() to CommandRunOut(output = "abc xyz".asSequence()),
            "abc${sep()}".asSequence() to CommandRunOut(output = "abc${sep()}".asSequence()),
            "abc${sep()}xyz".asSequence() to CommandRunOut(output = "abc${sep()}xyz".asSequence()),
        ).asArguments()

        @JvmStatic
        fun fileTexts() = listOf(
            "",
            "abc",
            "abc xyz",
            "abc${sep()}",
            "abc${sep()}xyz",
            "abc${sep()}xyz${sep()}12345"
        )

        @JvmStatic
        fun inputs() = listOf(
            null,
            "",
            "abc",
            "abc xyz",
            "abc${sep()}",
            "abc${sep()}xyz",
            "abc${sep()}xyz${sep()}12345"
        ).map { it?.asSequence() }

        @JvmStatic
        fun fileTextsWithInputs() = (fileTexts() * inputs()).asArguments()
    }
}
