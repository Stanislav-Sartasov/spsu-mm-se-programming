package minibash.parsing

interface Parser {

    fun parse(line: String): Instruction
}
