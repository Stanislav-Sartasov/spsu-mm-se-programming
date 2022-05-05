package minibash.pipe.commands

import kotlinx.coroutines.flow.Flow
import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.FlowUtils.concat
import minibash.utils.FlowUtils.toCharFlow
import minibash.utils.StringUtils.createErrorMessage
import minibash.utils.StringUtils.createNoArgumentsErrorMessage
import java.io.File

class CatCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Flow<Char>?) = if (args.isNotEmpty()) {
        args.fold(initial = CommandRunOut()) { (accOutput, accErrors), arg ->
            try {
                val text = File(arg).readText()
                CommandRunOut(
                    output = concat(accOutput, text.toCharFlow()),
                    errors = accErrors
                )
            } catch (e: Throwable) {
                CommandRunOut(
                    output = accOutput,
                    errors = concat(accErrors, createErrorMessage(name, e).toCharFlow())
                )
            }
        }
    } else {
        if (input != null) {
            CommandRunOut(output = input)
        } else {
            CommandRunOut(errors = createNoArgumentsErrorMessage(name).toCharFlow())
        }
    }
}
