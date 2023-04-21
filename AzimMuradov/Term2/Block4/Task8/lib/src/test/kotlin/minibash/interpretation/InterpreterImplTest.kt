package minibash.interpretation

import minibash.TestUtils.asArguments
import minibash.parsing.Instruction
import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.left
import minibash.utils.right
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import minibash.parsing.ExpandableString.Quoted as quoted
import minibash.parsing.ExpandableString.Variable as variable
import minibash.parsing.ExpandableString.Word as word

internal class InterpreterImplTest {

    @ParameterizedTest
    @MethodSource("argsForInterpretInstructionTest")
    fun `interpret instruction`(
        commands: List<Command>,
        instruction: Instruction,
        variables: Map<String, String>,
        interpretationOut: InterpretationOut,
    ) {
        val (variable, output, errors, signal) = InterpreterImpl(commands).interpret(instruction, variables)

        assertEquals(expected = interpretationOut.variable, actual = variable)
        assertContentEquals(expected = interpretationOut.output, actual = output)
        assertContentEquals(expected = interpretationOut.errors, actual = errors)
        assertEquals(expected = interpretationOut.signal, actual = signal)
    }


    private companion object {

        val outCmd = object : Command {

            override val name: String = "out"

            override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(output = "out".asSequence())
        }


        @JvmStatic
        fun argsForInterpretInstructionTest() = listOf<List<Any?>>(
            listOf(
                emptyList<Command>(),
                Instruction.None,
                emptyMap<String, String>(),
                InterpretationOut()
            ),
            listOf(
                emptyList<Command>(),
                Instruction.VariableAssignment(name = "var", value = word(value = "value")),
                emptyMap<String, String>(),
                InterpretationOut(variable = "var" to "value")
            ),
            listOf(
                emptyList<Command>(),
                Instruction.VariableAssignment(name = "var", value = variable(name = "x")),
                mapOf("x" to "value"),
                InterpretationOut(variable = "var" to "value")
            ),
            listOf(
                emptyList<Command>(),
                Instruction.VariableAssignment(
                    name = "var",
                    value = quoted(listOf("val".right(), variable("x").left()))
                ),
                mapOf("x" to "ue"),
                InterpretationOut(variable = "var" to "value")
            ),
            listOf(
                emptyList<Command>(),
                Instruction.SyntaxError("error"),
                emptyMap<String, String>(),
                InterpretationOut(errors = "error".asSequence())
            ),
            listOf(
                emptyList<Command>(),
                Instruction.Pipe(emptyList()),
                emptyMap<String, String>(),
                InterpretationOut()
            ),
            listOf(
                listOf(outCmd),
                Instruction.Pipe(listOf("out" to emptyList())),
                emptyMap<String, String>(),
                InterpretationOut(output = "out".asSequence())
            ),
        ).asArguments(n = 4)
    }
}
