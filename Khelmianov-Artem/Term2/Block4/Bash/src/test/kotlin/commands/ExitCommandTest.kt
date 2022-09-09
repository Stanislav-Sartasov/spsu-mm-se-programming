package commands

import bash.Environment
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import utils.BashExitException
import java.io.InputStream
import java.io.OutputStream

internal class ExitCommandTest {
    private val env = Environment().apply { exitCode = 42 }

    @Test
    fun noExitCode() {
        val exception = assertThrows<BashExitException> {
            ExitCommand(listOf()).run(
                InputStream.nullInputStream(),
                OutputStream.nullOutputStream(),
                OutputStream.nullOutputStream(),
                env
            )
        }
        assertEquals(42, exception.exitCode)
    }

    @Test
    fun withExitCode() {
        val exception = assertThrows<BashExitException> {
            ExitCommand(listOf("66")).run(
                InputStream.nullInputStream(),
                OutputStream.nullOutputStream(),
                OutputStream.nullOutputStream(),
                env
            )
        }
        assertEquals(66, exception.exitCode)
    }
    @Test
    fun invalidExitCode() {
        val exception = assertThrows<BashExitException> {
            ExitCommand(listOf("_")).run(
                InputStream.nullInputStream(),
                OutputStream.nullOutputStream(),
                OutputStream.nullOutputStream(),
                env
            )
        }
        assertEquals(42, exception.exitCode)
    }
}