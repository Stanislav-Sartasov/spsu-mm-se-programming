package minibash.pipe

interface Command {

    val name: String

    fun run(args: List<String>, input: Sequence<Char>? = null): CommandRunOut
}
