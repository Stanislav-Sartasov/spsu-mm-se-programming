package minibash.pipe

import kotlinx.coroutines.flow.Flow

interface Command {

    val name: String

    fun run(args: List<String>, input: Flow<Byte>? = null): CommandRunOut
}
