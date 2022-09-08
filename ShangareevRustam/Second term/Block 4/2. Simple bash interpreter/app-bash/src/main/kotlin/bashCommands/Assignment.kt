package bashCommands

import simpleBashInterpreter.textUtilities.TextUtilities.isValuable
import simpleBashInterpreter.textUtilities.TextUtilities.trimQuotes

object Assignment : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String {
		val argument = arg.slice(indices = 0 until arg.indexOf('='))
		val value = trimQuotes(arg.slice(indices = arg.indexOf('=') + 1 until arg.length))

		if (argument.isNotEmpty()) {
			arguments["\$$argument"] = value
		}
		return ""
	}

	override fun isValid(command: String) =
		command.indexOf('=') != -1 &&
				command.slice(0 until command.indexOf('=')).all { it.isLetterOrDigit() || it == '_' } &&
				isValuable(command.slice(command.indexOf('=') + 1 until command.length))
}
