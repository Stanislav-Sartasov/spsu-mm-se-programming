package minibash.pipe

import kotlinx.coroutines.flow.Flow

data class CommandRunOut(
    val output: Flow<Char>? = null,
    val errors: Flow<Char>? = null,
    val signal: Signal? = null,
)
