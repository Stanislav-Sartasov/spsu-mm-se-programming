package interpreter

import io.mockk.every
import io.mockk.mockk
import io.mockk.verify
import lib.interpreter.Interpreter
import lib.pipe.Output
import lib.pipe.commands.ICommand
import org.junit.jupiter.api.Test
import org.kodein.di.DI
import org.kodein.di.bindSingleton
import org.kodein.di.instance
import kotlin.test.assertEquals

class TestInterpreter {
    val commands = DI {
        bindSingleton<ICommand>("err") { mockk<ICommand>() }
        bindSingleton<ICommand>("out") { mockk<ICommand>() }
        bindSingleton<ICommand>("in") { mockk<ICommand>() }
    }

    init {
        val err: ICommand by commands.instance("err")
        every { err.run(any(), any()) } returns Output(null, "err1")
        val out: ICommand by commands.instance("out")
        every { out.run(any(), any()) } returns Output("out1", null)
        val inp: ICommand by commands.instance("in")
        every { inp.run(any(), any()) } returns Output("out2", "err2")
    }

    @Test
    fun `Test init var`() {
        val interpreter = Interpreter()
        interpreter.run("\$a=11", commands)
        assertEquals("11", interpreter.variables["\$a"])
    }

    @Test
    fun `Test pipe`() {
        val inp: ICommand by commands.instance("in")
        val out: ICommand by commands.instance("out")
        val err: ICommand by commands.instance("err")

        val interpreter = Interpreter()
        val output = interpreter.run("out a | in 'adad d' d | err", commands)
        verify { out.run(arrayOf("a"), any()) }
        verify { inp.run(arrayOf("adad d", "d"), "out1") }

        assertEquals("out2", output.output)
        assertEquals("err2", output.error)
        assert(!output.exit)
    }
}