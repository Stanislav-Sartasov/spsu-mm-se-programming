package minibash.pipe

data class CommandRunOut(
    val output: Sequence<Char>? = null,
    val errors: Sequence<Char>? = null,
    val signal: Signal? = null,
)
