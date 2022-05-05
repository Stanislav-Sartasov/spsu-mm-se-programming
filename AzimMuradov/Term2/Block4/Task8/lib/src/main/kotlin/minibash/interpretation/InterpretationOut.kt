package minibash.interpretation

import kotlinx.coroutines.flow.Flow
import minibash.pipe.Signal

data class InterpretationOut(
    val variable: Pair<String, String>? = null,
    val output: Flow<Char>? = null,
    val errors: Flow<Char>? = null,
    val signal: Signal? = null,
)
