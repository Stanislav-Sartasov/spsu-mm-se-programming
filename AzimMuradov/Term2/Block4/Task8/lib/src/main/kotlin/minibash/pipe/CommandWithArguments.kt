package minibash.pipe

data class CommandWithArguments(
    val command: Command,
    val arguments: List<String>,
)
