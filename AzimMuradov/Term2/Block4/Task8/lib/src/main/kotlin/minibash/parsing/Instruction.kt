package minibash.parsing

sealed interface Instruction {

    data class VariableAssignment(val name: String, val value: ExpandableString) : Instruction

    data class Pipe(val commandsWithArguments: List<Pair<String, List<ExpandableString>>>) : Instruction

    object None : Instruction

    data class SyntaxError(val message: String) : Instruction
}
