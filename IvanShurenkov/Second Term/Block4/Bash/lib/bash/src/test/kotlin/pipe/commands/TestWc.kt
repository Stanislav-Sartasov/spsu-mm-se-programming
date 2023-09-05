package pipe.commands

import lib.pipe.commands.WcCommand
import org.junit.jupiter.api.Test
import java.io.File
import java.io.FileNotFoundException
import kotlin.test.assertEquals
import kotlin.test.assertFails
import kotlin.test.assertNull

class TestWc {
    val sep = System.lineSeparator()
    @Test
    fun `Test cat command open exist file`() {
        val filename = "src/test/resources/testFile"
        val text = File(filename).readText()
        try {
            val countBytes = text.toString().toByteArray().count()
            val ans = "3 6 $countBytes $filename$sep"
            val wc = WcCommand("wc")
            val output = wc.run(arrayOf("src/test/resources/testFile"))
            assertNull(output.error)
            assertEquals(ans, output.output)
            assert(!output.exit)
        }
        catch (e: Exception) {
            assertFails { e.message }
        }
    }

    @Test
    fun `Test cat command open don't exist file`() {
        val wc = WcCommand("wc")
        val output = wc.run(arrayOf("testFile"))
        try {
            val text = File("testFile").readText()
        } catch (e: FileNotFoundException) {
            assertEquals(e.message + sep, output.error)
        }
        assertNull(output.output)
        assert(!output.exit)
    }
}