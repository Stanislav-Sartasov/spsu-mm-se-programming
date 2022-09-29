package commands

import bash.Environment
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import java.io.ByteArrayInputStream
import java.io.ByteArrayOutputStream
import java.io.File
import kotlin.test.assertContains
import kotlin.test.assertEquals

internal class CatCommandTest {
    private lateinit var input: ByteArrayInputStream
    private lateinit var output: ByteArrayOutputStream
    private lateinit var error: ByteArrayOutputStream
    private lateinit var file: File
    private val env = Environment()

    @BeforeEach
    fun setUp() {
        file = File.createTempFile("test", null).apply { writeText("file\nwith text") }
        input = ByteArrayInputStream(
            """
            |input stream
            |with text
        """.trimMargin().toByteArray()
        )
        output = ByteArrayOutputStream()
        error = ByteArrayOutputStream()
    }

    @AfterEach
    fun tearDown() {
        file.delete()
    }

    @Test
    fun fromInputStream() {
        val cmd = CatCommand(listOf())
        val exitCode = cmd.run(input, output, error, env)
        assertEquals(0, exitCode)
        assertEquals("input stream\nwith text", output.toString())
        assertEquals("", error.toString())
    }

    @Test
    fun fromFile() {
        val cmd = CatCommand(listOf(file.path))
        val exitCode = cmd.run(input, output, error, env)
        assertEquals(0, exitCode)
        assertEquals("file\nwith text", output.toString())
        assertEquals("", error.toString())
    }

    @Test
    fun fileWithoutReadPermission() {
        file.setReadable(false)
        val cmd = CatCommand(listOf(file.path))
        val exitCode = cmd.run(input, output, error, env)
        assertEquals(1, exitCode)
        assertEquals("", output.toString())
        assertContains(error.toString(), "[cat] Error while reading file")
    }

    @Test
    fun fileDoesNotExist() {
        val cmd = CatCommand(listOf(file.path + "_"))
        val exitCode = cmd.run(input, output, error, env)
        assertEquals(1, exitCode)
        assertEquals("", output.toString())
        assertContains(error.toString(), "[cat] Error while reading file")
    }
}