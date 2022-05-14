package minibash.interpretation

import minibash.pipe.Signal

data class InterpretationOut(
    val variable: Pair<String, String>? = null,
    val output: Sequence<Char>? = null,
    val errors: Sequence<Char>? = null,
    val signal: Signal? = null,
)
