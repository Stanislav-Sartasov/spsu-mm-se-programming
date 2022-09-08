package simpleBashInterpreter

import bashCommands.*
import simpleBashInterpreter.SimpleBashTextProcessor.preprocessCommand

class SimpleBashInterpreter {

	private val arguments = mutableMapOf<String, String>()

	private val commands = listOf(Assignment, Cat, Echo, Exit, Help, Pwd, Wc, OsExec)

	fun interpret(command: String, input: String): String? {
		val processedCommand = preprocessCommand(command, arguments)

		var result = input
		for (instruction in processedCommand) {
			for (curCommand in commands) {
				if (curCommand.isValid(instruction)) {
					val curResult = curCommand.run(instruction, result, arguments)
					if (curResult == null && processedCommand.size == 1) {
						return null
					} else {
						result = curResult ?: ""
					}
					break
				}
			}
		}

		return result
	}

}
