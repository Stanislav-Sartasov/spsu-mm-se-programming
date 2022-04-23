package minibash.pipe

import kotlinx.coroutines.flow.Flow

data class CommandRunOut(
    val output: Flow<Byte>? = null,
    val errors: Flow<Byte>? = null,
    val signal: Signal? = null,
)
