package pipe.commands

import lib.pipe.commands.WcCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestWc {
    @Test
    fun `Test cat command open exist file`() {
        val ans = "3 6 11 src/test/resources/testFile\n"
        val wc = WcCommand("wc")
        val output = wc.run(arrayOf("src/test/resources/testFile"))
        assertNull(output.error)
        assertEquals(ans, output.output)
        assert(!output.exit)
    }

    @Test
    fun `Test cat command open don't exist file`() {
        val wc = WcCommand("wc")
        val output = wc.run(arrayOf("testFile"))
        assertEquals("testFile (Нет такого файла или каталога)\n", output.error)
        assertNull(output.output)
        assert(!output.exit)
    }
}