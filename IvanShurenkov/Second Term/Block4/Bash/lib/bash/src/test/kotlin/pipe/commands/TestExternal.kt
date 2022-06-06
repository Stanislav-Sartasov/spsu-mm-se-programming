package pipe.commands

import lib.pipe.commands.ExternalCommand
import lib.pipe.commands.WcCommand
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotEquals
import kotlin.test.assertNotNull
import kotlin.test.assertNull

class TestExternal {
    val external = ExternalCommand("cmd")
    @Test
    fun `Test external command without input`() {
        val output = external.run(arrayOf("echo", "test"))
        assertNotNull(output.output!!.split("\\s+".toRegex()))
        assertEquals("", output.error)
        assert(!output.exit)
    }

    @Test
    fun `Test don't exist command`() {
        val output = ExternalCommand("grob").run(emptyArray())
        assertNull(output.output)
        assertNotNull(output.error)
        assertNotEquals("", output.error)
        assert(!output.exit)
    }
}