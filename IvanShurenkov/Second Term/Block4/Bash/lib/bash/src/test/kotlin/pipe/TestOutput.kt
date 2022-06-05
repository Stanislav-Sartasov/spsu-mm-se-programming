package pipe

import lib.pipe.Output
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestOutput {
    val sep = System.lineSeparator()
    @Test
    fun `Test add fun`() {
        val output = Output()
        assertNull(output.output)
        assertNull(output.error)
        assert(!output.exit)

        output.addOut(null)
        output.addErr(null)
        assertNull(output.output)
        assertNull(output.error)
        assert(!output.exit)

        output.addOut("1")
        output.addErr("2")
        assertEquals("1$sep", output.output)
        assertEquals("2$sep", output.error)
        assert(!output.exit)

        output.addOut(null)
        output.addErr(null)
        assertEquals("1$sep", output.output)
        assertEquals("2$sep", output.error)
        assert(!output.exit)
    }
}