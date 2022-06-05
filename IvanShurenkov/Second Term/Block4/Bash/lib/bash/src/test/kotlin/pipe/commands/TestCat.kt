package pipe.commands

import lib.pipe.commands.CatCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull
import java.io.*

class TestCat {
    val sep = System.lineSeparator()
    @Test
    fun `Test cat command open exist file`() {
        val ans = "a${sep}b${sep}c${sep}d e f${sep}"
        val cat = CatCommand("cat")
        val output = cat.run(arrayOf("src/test/resources/testFile"))
        assertNull(output.error)
        assertEquals(ans, output.output)
        assert(!output.exit)
    }

    @Test
    fun `Test cat command open don't exist file`() {
        val cat = CatCommand("cat")
        val output = cat.run(arrayOf("testFile"))
        try {
            val text = File("testFile").readText()
        } catch (e: FileNotFoundException) {
            assertEquals(e.message + sep, output.error)
        }
        assertNull(output.output)
        assert(!output.exit)
    }
}