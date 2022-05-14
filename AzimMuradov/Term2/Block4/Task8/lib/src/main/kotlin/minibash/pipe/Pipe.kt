package minibash.pipe

interface Pipe {

    fun run(
        commandsWithArguments: List<CommandWithArguments>,
        input: Sequence<Char>? = null,
    ): CommandRunOut
}
