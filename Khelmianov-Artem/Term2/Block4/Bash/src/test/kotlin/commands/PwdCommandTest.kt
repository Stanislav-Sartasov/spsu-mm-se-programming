package commands

import bash.Environment
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import java.io.ByteArrayOutputStream
import java.io.InputStream
import java.io.OutputStream

internal class PwdCommandTest {

    @Test
    fun run() {
        val expected = System.getProperty("user.dir") + "\n"
        val actual = ByteArrayOutputStream().also { out ->
            PwdCommand(listOf()).run(
                InputStream.nullInputStream(),
                out,
                OutputStream.nullOutputStream(),
                Environment()
            )
        }.toString()
        assertEquals(expected, actual)
    }
}