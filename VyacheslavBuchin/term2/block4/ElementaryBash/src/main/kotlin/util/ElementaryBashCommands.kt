package util

import command.*
import service.command.CommandManager
import service.command.MapCommandManager
import service.substitution.MapSubstitutionManager
import service.substitution.SubstitutionManager

class ElementaryBashCommands private constructor() {
	companion object {
		fun getBuiltInCommandsManager(substitutionManager: SubstitutionManager = MapSubstitutionManager()): CommandManager {
			val commandManager = MapCommandManager(substitutionManager)
			commandManager["cat"] = { CatCommand() }
			commandManager["echo"] = { EchoCommand() }
			commandManager["exit"] = { ExitCommand() }
			commandManager["pwd"] = { PwdCommand() }
			commandManager["wc"] = { WcCommand() }
			return commandManager
		}
	}
}