package minibash.pipe.commands

import minibash.pipe.*

class ExitCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(signal = Signal.SIGINT)
}
