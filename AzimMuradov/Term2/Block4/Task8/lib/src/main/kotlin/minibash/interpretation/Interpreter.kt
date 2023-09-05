package minibash.interpretation

import minibash.parsing.Instruction

interface Interpreter {

    fun interpret(
        instruction: Instruction,
        variables: Map<String, String>,
    ): InterpretationOut
}
