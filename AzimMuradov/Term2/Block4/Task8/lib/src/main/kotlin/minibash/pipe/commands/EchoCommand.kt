package minibash.pipe.commands

import kotlinx.coroutines.flow.Flow
import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.FlowUtils.toCharFlow
import minibash.utils.StringUtils.ln

class EchoCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Flow<Char>?) = CommandRunOut(
        output = args.joinToString(separator = " ").ln().toCharFlow()
    )
}
