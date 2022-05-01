package minibash.parsing

sealed interface Instruction {

    data class AssignVariable(val name: String, val value: ExpandableString) : Instruction

    data class RunPipe(val commandsWithArguments: List<Pair<String, List<ExpandableString>>>) : Instruction

    data class ThrowSyntaxError(val message: String) : Instruction
}
