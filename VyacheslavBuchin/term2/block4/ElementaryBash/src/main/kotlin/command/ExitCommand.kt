package command

import channel.Channel
import channel.StringChannel
import exception.ElementaryBashException
import kotlin.system.exitProcess

class ExitCommand(
	override var input: Channel<String> = StringChannel.nullChannel()
) : Command {
	override var output = StringChannel.nullChannel()
	override var error: Channel<String> = StringChannel()

	override fun execute(args: Array<String>): Int {
		if (args.isEmpty())
			exitProcess(0)
		if (args.size > 1)
			throw ElementaryBashException(ElementaryBashException.INVALID_ARGUMENTS, "too many arguments")
		if (args[0].toIntOrNull() == null)
			throw ElementaryBashException(ElementaryBashException.INVALID_ARGUMENTS, "${args[0]}: numeric argument required")
		exitProcess(args[0].toInt())
	}

}