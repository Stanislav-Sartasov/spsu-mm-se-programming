package bashCommands

abstract class BashCommand {

	abstract fun run(arg: String, input: String, arguments: MutableMap<String, String>): String?

	abstract fun isValid(command: String): Boolean

}