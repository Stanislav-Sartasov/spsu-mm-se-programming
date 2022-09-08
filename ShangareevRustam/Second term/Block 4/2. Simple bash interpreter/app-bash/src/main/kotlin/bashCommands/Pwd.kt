package bashCommands

object Pwd : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String {
		return System.getProperty("user.dir")
	}

	override fun isValid(command: String) =
		command.split(" ").any { it != "" } &&
				command.split(" ").first { it != "" } == "pwd"

}