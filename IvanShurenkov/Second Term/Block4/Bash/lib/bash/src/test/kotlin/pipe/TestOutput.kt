package pipe

import lib.pipe.Output
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestOutput {
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
        assertEquals("1\n", output.output)
        assertEquals("2\n", output.error)
        assert(!output.exit)

        output.addOut(null)
        output.addErr(null)
        assertEquals("1\n", output.output)
        assertEquals("2\n", output.error)
        assert(!output.exit)
    }
}