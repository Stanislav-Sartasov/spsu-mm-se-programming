package minibash.pipe

import kotlinx.coroutines.flow.Flow
import minibash.utils.FlowUtils.concat

object PipeImpl : Pipe {

    override fun run(
        commandsWithArguments: List<CommandWithArguments>,
        input: Flow<Char>?,
    ): CommandRunOut {
        if (commandsWithArguments.isEmpty()) return CommandRunOut()

        return commandsWithArguments.fold(
            initial = CommandRunOut(output = input)
        ) { (input, accErrors), (cmd, args) ->
            val (output, errors, signal) = cmd.run(args, input)

            when (signal) {
                null -> CommandRunOut(
                    output = output,
                    errors = concat(accErrors, errors)
                )
                Signal.SIGINT -> return CommandRunOut(
                    output = output,
                    errors = concat(accErrors, errors),
                    signal = signal
                )
            }
        }
    }
}
