package minibash.interpretation

import minibash.parsing.ExpandableString
import minibash.parsing.Instruction
import minibash.pipe.*
import minibash.pipe.commands.ExternalCommand
import minibash.utils.fold

class InterpreterImpl(private val availableCommands: List<Command>) : Interpreter {

    override fun interpret(instruction: Instruction, variables: Map<String, String>) = when (instruction) {
        is Instruction.VariableAssignment -> InterpretationOut(
            variable = instruction.run { name to value.expand(variables) }
        )
        is Instruction.Pipe -> {
            val (output, errors, signal) = PipeImpl.run(
                commandsWithArguments = instruction.commandsWithArguments.map { (cmdName, expandableArgs) ->
                    CommandWithArguments(
                        command = availableCommands.find { it.name == cmdName } ?: ExternalCommand(cmdName),
                        arguments = expandableArgs.map { it.expand(variables) }
                    )
                },
            )

            InterpretationOut(output = output, errors = errors, signal = signal)
        }
        Instruction.None -> InterpretationOut()
        is Instruction.SyntaxError -> InterpretationOut(errors = instruction.message.asSequence())
    }


    private fun ExpandableString.expand(variables: Map<String, String>) = when (this) {
        is ExpandableString.Quoted -> content.joinToString(separator = "") { varOrWord ->
            varOrWord.fold(
                onLeft = { variables[it.name] ?: System.getenv()[it.name] ?: "" },
                onRight = { it }
            )
        }
        is ExpandableString.Variable -> variables[name] ?: System.getenv()[name] ?: ""
        is ExpandableString.Word -> value
    }
}
