package interpreter

import channel.Channel
import command.Command
import exception.ElementaryBashException
import service.command.CommandManager

class ElementaryBashInterpreter(
	private val commandManager: CommandManager,
	override val input: Channel<String>,
	override val output: Channel<String>,
	override val error: Channel<String>
) : Interpreter {
	override fun interpret(commandsWithArgs: List<List<String>>): Int {
		if (commandsWithArgs.isEmpty())
			return 0
		val commands = getCommands(commandsWithArgs)
		makePipes(commands)
		for (i in commands.indices) {
			try {
				val command = commands[i]
				val argsBeginIndex = if (commandsWithArgs[i][0].contains("=")) 0 else 1
				val args = commandsWithArgs[i].subList(argsBeginIndex, commandsWithArgs[i].size).toTypedArray()

				val exitCode = command.execute(args)

				val error = command.error.read()
				if (error.trim() != "")
					this.error.write(error)

				if (exitCode != 0)
					return exitCode
			} catch (exception: ElementaryBashException) {
				throw ElementaryBashException(commandsWithArgs[i].first(),
					"${exception.name}${if (exception.message.isNotEmpty()) ": ${exception.message}" else ""}"
				)
			}
		}
		return 0
	}

	private fun getCommands(commandsWithArgs: List<List<String>>) =
		commandsWithArgs.map { commandManager[it.first()] }

	private fun makePipes(commands: List<Command>) {
		commands.first().input = input
		for (i in 0..(commands.size - 2)) {
			commands[i].pipeTo(commands[i + 1])
		}
		commands.last().output = output
	}

	private fun Command.pipeTo(to: Command) {
		to.input = this.output
	}
}