package command

import channel.Channel
import channel.StringChannel
import kotlin.system.exitProcess

class ExitCommand(
	override val input: Channel<String> = StringChannel.nullChannel()
) : Command {
	override val output = StringChannel.nullChannel()
	override val error = StringChannel()

	override fun execute(args: Array<String>): Int {
		if (args.isEmpty())
			exitProcess(0)
		if (args.size > 1) {
			error.write("too many arguments")
			return 1
		}
		if (args[0].toIntOrNull() == null) {
			error.write("${args[0]}: numeric argument required")
			return 1
		}
		exitProcess(args[0].toInt())
	}

}