package bashCommands

import simpleBashInterpreter.textUtilities.TextUtilities.trimQuotes
import java.io.BufferedReader
import java.io.File
import java.io.FileReader

object Cat : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String {
		if (arg.split(" ").none { it != "" && it != "cat" }) return input
		val interpolatedArg = trimQuotes(arg.slice(arg.indexOf("cat") + 4 until arg.length).trim())

		val file = File(interpolatedArg)
		if (!file.exists()) return "cat: $interpolatedArg: No such file or directory"
		if (file.isDirectory) return "cat: $interpolatedArg: Is a directory"

		val result = StringBuilder()
		try {
			val bufferedReader = BufferedReader(FileReader(file))
			while (true) {
				val line = bufferedReader.readLine()
				result.append(if (line == null) break else "$line\n")
			}
			bufferedReader.close()
		} catch (e: Exception) {
			return "cat: $interpolatedArg: An error occurred while reading the file"
		}
		return result.toString()
	}

	override fun isValid(command: String) =
		command.split(" ").any { it != "" } &&
				command.split(" ").first { it != "" } == "cat"

}