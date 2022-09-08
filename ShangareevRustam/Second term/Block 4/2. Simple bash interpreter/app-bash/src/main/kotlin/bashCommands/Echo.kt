package bashCommands

import simpleBashInterpreter.textUtilities.TextUtilities.trimQuotes

object Echo : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String {
		return trimQuotes(arg.slice(arg.indexOf("echo") + 5 until arg.length))
	}

	override fun isValid(command: String) =
		command.split(" ").any { it != "" } &&
		command.split(" ").first { it != "" } == "echo"

}