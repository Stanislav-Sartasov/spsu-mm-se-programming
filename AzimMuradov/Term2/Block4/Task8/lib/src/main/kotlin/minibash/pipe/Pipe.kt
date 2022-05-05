package minibash.pipe

import kotlinx.coroutines.flow.Flow

interface Pipe {

    fun run(
        commandsWithArguments: List<CommandWithArguments>,
        input: Flow<Char>? = null,
    ): CommandRunOut
}
