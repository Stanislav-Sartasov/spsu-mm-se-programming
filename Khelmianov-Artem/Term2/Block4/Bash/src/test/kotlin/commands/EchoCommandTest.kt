package commands

import bash.Environment
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import java.io.ByteArrayOutputStream
import java.io.InputStream

internal class EchoCommandTest {

    private var input = InputStream.nullInputStream()
    private lateinit var output: ByteArrayOutputStream
    private lateinit var error: ByteArrayOutputStream
    private val env = Environment()

    @BeforeEach
    fun setUp() {
        output = ByteArrayOutputStream()
        error = ByteArrayOutputStream()
    }

    @Test
    fun fromInputStream() {
        val cmd = EchoCommand(listOf("abc\n", "def", "   xyz"))
        val exitCode = cmd.run(input, output, error, env)
        kotlin.test.assertEquals(0, exitCode)
        kotlin.test.assertEquals("abc\n def    xyz\n", output.toString())
        kotlin.test.assertEquals("", error.toString())
    }
}