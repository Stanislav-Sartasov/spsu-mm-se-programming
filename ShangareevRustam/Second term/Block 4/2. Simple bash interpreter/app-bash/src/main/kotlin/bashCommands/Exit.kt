package bashCommands

object Exit : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>) = null

	override fun isValid(command: String) =
		command.split(" ").any { it != "" } &&
		command.split(" ").first { it != "" } == "exit"

}