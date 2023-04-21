package commands

import bash.Environment
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import java.io.ByteArrayInputStream
import java.io.ByteArrayOutputStream
import java.io.File
import kotlin.test.assertContains

internal class WcCommandTest {
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
        val cmd = WcCommand(listOf())
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(0, exitCode)
        kotlin.test.assertEquals("1\t4\t22", output.toString())
        kotlin.test.assertEquals("", error.toString())
    }

    @Test
    fun fromFile() {
        val cmd = WcCommand(listOf(file.path))
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(0, exitCode)
        kotlin.test.assertEquals("1\t3\t14\t${file.path}\n", output.toString())
        kotlin.test.assertEquals("", error.toString())
    }

    @Test
    fun fromMultipleFiles() {
        val cmd = WcCommand(listOf(file.path, file.path))
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(0, exitCode)
        kotlin.test.assertEquals("1\t3\t14\t${file.path}\n".repeat(2) + "2\t6\t28\tSummary", output.toString())
        kotlin.test.assertEquals("", error.toString())
    }

    @Test
    fun fileWithoutReadPermission() {
        file.setReadable(false)
        val cmd = WcCommand(listOf(file.path))
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(1, exitCode)
        kotlin.test.assertEquals("", output.toString())
        assertContains(error.toString(), "[wc] Error while reading file")
    }

    @Test
    fun fileDoesNotExist() {
        val cmd = WcCommand(listOf(file.path + "_"))
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(1, exitCode)
        kotlin.test.assertEquals("", output.toString())
        assertContains(error.toString(), "[wc] Error while reading file")
    }
}