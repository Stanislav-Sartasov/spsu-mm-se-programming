package minibash

import io.mockk.every
import io.mockk.mockk
import minibash.TestUtils.asArguments
import minibash.interpretation.InterpretationOut
import minibash.interpretation.Interpreter
import minibash.parsing.Instruction
import minibash.parsing.Parser
import minibash.pipe.Signal
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import java.io.*
import kotlin.test.assertEquals
import java.lang.System.lineSeparator as sep

internal class MiniBashImplTest {

    @ParameterizedTest
    @MethodSource("argsForRunMiniBashTest")
    fun `run mini bash`(
        input: String,
        output: String?,
        errors: String?,
        interpretationOut: InterpretationOut,
    ) {
        val parser = mockk<Parser>()
        every { parser.parse(any()) } returns Instruction.None

        val interpreter = mockk<Interpreter>()
        every { interpreter.interpret(any(), any()) } returns interpretationOut.copy()


        val miniBash = MiniBashImpl(parser, interpreter)

        val out = ByteArrayOutputStream()
        val errs = ByteArrayOutputStream()

        miniBash.run(
            inputStream = ByteArrayInputStream(input.toByteArray()),
            outputStream = PrintStream(out),
            errorsStream = PrintStream(errs)
        )

        assertEquals(expected = output, actual = out.toString().takeUnless(String::isEmpty))
        assertEquals(expected = errors, actual = errs.toString().takeUnless(String::isEmpty))
    }


    private companion object {

        @JvmStatic
        fun argsForRunMiniBashTest() = listOf(
            listOf(
                " ",
                null,
                null,
                InterpretationOut()
            ),
            listOf(
                " ",
                null,
                null,
                InterpretationOut(variable = "var" to "val")
            ),
            listOf(
                "Hello, world!",
                "out",
                "err",
                InterpretationOut(output = "out".asSequence(), errors = "err".asSequence())
            ),
            listOf(
                " ${sep()} ${sep()}",
                "outout",
                "errerr",
                InterpretationOut(output = "out".asSequence(), errors = "err".asSequence())
            ),
            listOf(
                " ${sep()} ${sep()}",
                "out",
                "err",
                InterpretationOut(output = "out".asSequence(), errors = "err".asSequence(), signal = Signal.SIGINT)
            ),
        ).asArguments(n = 4)
    }
}
