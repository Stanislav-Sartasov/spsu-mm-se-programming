package minibash.pipe.commands

import kotlinx.coroutines.flow.Flow
import minibash.pipe.*

class ExitCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Flow<Char>?) = CommandRunOut(signal = Signal.SIGINT)
}
