package service.command

import command.AssignCommand
import command.Command
import command.OSCommand
import service.substitution.SubstitutionManager

class MapCommandManager(private val substitutionManager: SubstitutionManager) : CommandManager {
	private val commands = mutableMapOf<String, () -> Command>()
	override fun get(name: String): Command {
		if (name.contains("=")) {
			return AssignCommand(substitutionManager)
		}
		return commands[name]?.invoke() ?: OSCommand(name)
	}

	override fun set(name: String, factory: () -> Command) {
		commands[name] = factory
	}
}