package interpreter

import io.mockk.every
import io.mockk.mockk
import io.mockk.verify
import lib.interpreter.Instruction
import lib.interpreter.parser.Block
import lib.pipe.Output
import lib.pipe.commands.ExternalCommand
import lib.pipe.commands.ICommand
import org.junit.jupiter.api.Test
import org.kodein.di.DI
import org.kodein.di.bindSingleton
import org.kodein.di.instance
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestInstruction {
    @Test
    fun `Test run without args`() {
        val output = Instruction(emptyList(), DI {}).run(null, emptyMap())
        assertNull(output.output.output)
        assertNull(output.output.error)
        assert(!output.output.exit)
        assertNull(output.variable)
    }

    @Test
    fun `Test init var`() {
        val output = Instruction(listOf(Block("\$a"), Block("="), Block("b")), DI {}).run(null, emptyMap())
        assertNull(output.output.output)
        assertNull(output.output.error)
        assert(!output.output.exit)
        assertEquals(("\$a" to "b"), output.variable)
    }

    @Test
    fun `Test get not existed var`() {
        val output = Instruction(listOf(Block("\$a")), DI {}).run(null, emptyMap())
        assertNull(output.output.output)
        assertNull(output.output.error)
        assert(!output.output.exit)
        assertNull(output.variable)
    }

    @Test
    fun `Test var as command without args`() {
        val commands = DI {
            bindSingleton<ICommand>("com") { mockk<ICommand>() }
        }
        val com: ICommand by commands.instance("com")
        every { com.run(any(), any()) } returns Output("out", "err", true)
        val output = Instruction(listOf(Block("\$a")), commands).run("inp", mapOf(("\$a" to "com")))
        assertEquals("out", output.output.output)
        assertEquals("err", output.output.error)
        assert(output.output.exit)
        assertNull(output.variable)
        verify { com.run(emptyArray(), "inp") }
    }

    @Test
    fun `Test var as command with args`() {
        val commands = DI {
            bindSingleton<ICommand>("com") { mockk<ICommand>() }
        }
        val com: ICommand by commands.instance("com")
        every { com.run(any(), any()) } returns Output("out", "err", true)
        val output =
            Instruction(listOf(Block("\$a"), Block("a"), Block("b")), commands).run("inp", mapOf(("\$a" to "com")))
        assertEquals("out", output.output.output)
        assertEquals("err", output.output.error)
        assert(output.output.exit)
        assertNull(output.variable)
        verify { com.run(arrayOf("a", "b"), "inp") }
    }

    @Test
    fun `Test var as arg`() {
        val commands = DI {
            bindSingleton<ICommand>("com") { mockk<ICommand>() }
        }
        val com: ICommand by commands.instance("com")
        every { com.run(any(), any()) } returns Output("out", "err", true)
        val output =
            Instruction(listOf(Block("com"), Block("\$a"), Block("\$b")), commands).run("inp", mapOf(("\$a" to "c")))
        assertEquals("out", output.output.output)
        assertEquals("err", output.output.error)
        assert(output.output.exit)
        assertNull(output.variable)
        verify { com.run(arrayOf("c", "\$b"), "inp") }
    }

    @Test
    fun `Test run command`() {
        val commands = DI {
            bindSingleton<ICommand>("com") { mockk<ICommand>() }
        }
        val com: ICommand by commands.instance("com")
        every { com.run(any(), any()) } returns Output("out", "err", true)
        val output = Instruction(listOf(Block("com"), Block("a"), Block("b")), commands).run("inp", emptyMap())
        assertEquals("out", output.output.output)
        assertEquals("err", output.output.error)
        assert(output.output.exit)
        assertNull(output.variable)
        verify { com.run(arrayOf("a", "b"), "inp") }
    }

    @Test
    fun `Test run external command`() {
        val actualOut = Instruction(listOf(Block("ls")), DI {}).run("inp", emptyMap())
        val expectedOut = ExternalCommand("ls").run(emptyArray())
        assertEquals(expectedOut.output, actualOut.output.output)
        assertEquals(expectedOut.error, actualOut.output.error)
        assertEquals(expectedOut.exit, actualOut.output.exit)
    }
}