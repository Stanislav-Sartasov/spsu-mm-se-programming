package pipe.commands

import lib.pipe.commands.CatCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestCat {
    @Test
    fun `Test cat command open exist file`() {
        val ans = "a\nb\nc\nd e f\n"
        val cat = CatCommand("cat")
        val output = cat.run(arrayOf("src/test/resources/testFile"))
        assertNull(output.error)
        assertEquals(ans, output.output)
        assert(!output.exit)
    }

    @Test
    fun `Test cat command open don't exist file`() {
        val ans = "a\nb\nc\nd e f"
        val cat = CatCommand("cat")
        val output = cat.run(arrayOf("testFile"))
        assertEquals("testFile (Нет такого файла или каталога)\n", output.error)
        assertNull(output.output)
        assert(!output.exit)
    }
}