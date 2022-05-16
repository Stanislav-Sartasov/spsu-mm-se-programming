package service.command

import command.Command
import command.OSCommand

class MapCommandManager : CommandManager {
	private val commands = mutableMapOf<String, Command>()
	override fun get(name: String) = commands[name] ?: OSCommand(name)

	override fun set(name: String, command: Command) {
		commands[name] = command
	}
}