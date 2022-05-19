package pipe.commands

import lib.pipe.commands.ExternalCommand
import lib.pipe.commands.WcCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestExternal {
    @Test
    fun `Test external command without input`() {
        val ans = " 3  6 11 src/test/resources/testFile\n"
        val output = ExternalCommand("wc").run(arrayOf("src/test/resources/testFile"))
        assertEquals(ans.split("\\s+".toRegex()), output.output!!.split("\\s+".toRegex()))
        assertEquals("", output.error)
        assert(!output.exit)
    }

    @Test
    fun `Test external command with input`() {
        val output = ExternalCommand("grep").run(arrayOf("a"), "a\nb\nc\n")
        assertEquals("a\n", output.output)
        assertEquals("", output.error)
        assert(!output.exit)
    }

    @Test
    fun `Test don't exist command`() {
        val output = ExternalCommand("grob").run(emptyArray())
        assertNull(output.output)
        assertEquals("Cannot run program \"grob\": error=2, Нет такого файла или каталога\n", output.error)
        assert(!output.exit)
    }
}