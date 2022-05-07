package minibash.pipe

import minibash.TestUtils.asArguments
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import minibash.pipe.CommandWithArguments as CmdWithArgs

internal class PipeImplTest {

    @ParameterizedTest
    @MethodSource("argsForRunPipeTest")
    fun `run pipe`(
        commandsWithArguments: List<CmdWithArgs>,
        input: Sequence<Char>?,
        pipeRunOut: CommandRunOut,
    ) {
        val (output, errors, signal) = PipeImpl.run(commandsWithArguments, input)

        assertContentEquals(expected = pipeRunOut.output, actual = output)
        assertContentEquals(expected = pipeRunOut.errors, actual = errors)
        assertEquals(expected = pipeRunOut.signal, actual = signal)
    }


    private companion object {

        val outCmd = object : Command {

            override val name: String get() = error("")

            override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(output = "out".asSequence())
        }

        val errCmd = object : Command {

            override val name: String get() = error("")

            override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(errors = "err".asSequence())
        }

        val sigCmd = object : Command {

            override val name: String get() = error("")

            override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(signal = Signal.SIGINT)
        }

        val inputDependentCmd = object : Command {

            override val name: String get() = error("")

            override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(output = input)
        }


        @JvmStatic
        fun argsForRunPipeTest() = listOf(
            Triple(first = emptyList(), second = null, third = CommandRunOut()),
            Triple(
                first = listOf(CmdWithArgs(outCmd, emptyList())),
                second = null,
                third = CommandRunOut(output = "out".asSequence())
            ),
            Triple(
                first = listOf(CmdWithArgs(errCmd, emptyList())),
                second = null,
                third = CommandRunOut(errors = "err".asSequence())
            ),
            Triple(
                first = listOf(CmdWithArgs(sigCmd, emptyList())),
                second = null,
                third = CommandRunOut(signal = Signal.SIGINT)
            ),
            Triple(
                first = listOf(
                    CmdWithArgs(outCmd, emptyList()),
                    CmdWithArgs(outCmd, emptyList()),
                    CmdWithArgs(outCmd, emptyList()),
                ),
                second = null,
                third = CommandRunOut(output = "out".asSequence())
            ),
            Triple(
                first = listOf(
                    CmdWithArgs(errCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                ),
                second = null,
                third = CommandRunOut(errors = "errerrerr".asSequence())
            ),
            Triple(
                first = listOf(
                    CmdWithArgs(outCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                ),
                second = null,
                third = CommandRunOut(errors = "err".asSequence())
            ),
            Triple(
                first = listOf(
                    CmdWithArgs(outCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                    CmdWithArgs(outCmd, emptyList()),
                ),
                second = null,
                third = CommandRunOut(output = "out".asSequence(), errors = "err".asSequence())
            ),
            Triple(
                first = listOf(
                    CmdWithArgs(outCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                    CmdWithArgs(sigCmd, emptyList()),
                    CmdWithArgs(errCmd, emptyList()),
                ),
                second = null,
                third = CommandRunOut(errors = "err".asSequence(), signal = Signal.SIGINT)
            ),
            Triple(
                first = listOf(CmdWithArgs(inputDependentCmd, emptyList())),
                second = null,
                third = CommandRunOut()
            ),
            Triple(
                first = listOf(CmdWithArgs(inputDependentCmd, emptyList())),
                second = "out".asSequence(),
                third = CommandRunOut(output = "out".asSequence())
            ),
        ).asArguments()
    }
}
