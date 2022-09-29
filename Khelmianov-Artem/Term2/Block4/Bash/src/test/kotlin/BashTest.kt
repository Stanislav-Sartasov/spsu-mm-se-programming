import org.junit.jupiter.api.Test
import java.io.ByteArrayInputStream
import java.io.ByteArrayOutputStream
import java.io.File
import kotlin.test.assertEquals

internal class BashTest {
    @Test
    fun run() {
        val inf = File.createTempFile("test", null).also { it.writeText("file") }
        val outf = File.createTempFile("test", null)
        val input = ByteArrayInputStream(
            """hello="Hello, world!"
            echo "<${'$'}hello>" | wc | cat;
            cat < ${inf.absolutePath} | wc > ${outf.absolutePath}
            exit | echo unreachable""".trimMargin().toByteArray()
        )
        val output = ByteArrayOutputStream()
        val error = ByteArrayOutputStream()
        val exitCode = Bash(TestLogger()).main(input, output, error)
        assertEquals("1\t2\t16", output.toString())
        assertEquals("0\t1\t4", outf.readText())
        assertEquals("", error.toString())
        assertEquals(0, exitCode)
    }
}