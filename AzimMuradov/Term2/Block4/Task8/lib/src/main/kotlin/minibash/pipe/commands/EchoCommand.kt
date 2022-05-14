package minibash.pipe.commands

import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.StringUtils.ln

class EchoCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Sequence<Char>?) = CommandRunOut(
        output = args.joinToString(separator = " ").ln().asSequence()
    )
}
