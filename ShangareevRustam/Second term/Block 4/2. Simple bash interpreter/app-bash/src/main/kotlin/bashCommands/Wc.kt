package bashCommands

import textUtilities.TextUtilities.trimQuotes
import java.io.BufferedReader
import java.io.File
import java.io.FileReader

object Wc : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String {
		if (arg.split(" ").none { it != "" && it != "wc" }) {
			return "1 ${input.split(" ").size} ${input.length}"
		}
		val interpolatedArg = trimQuotes(arg.slice(arg.indexOf("wc") + 3 until arg.length).trim())

		val file = File(interpolatedArg)
		if (!file.exists()) return "wc: $interpolatedArg: No such file or directory"
		if (file.isDirectory) return "wc: $interpolatedArg: Is a directory\n 0 0 0 $interpolatedArg"

		val bufferedReader = BufferedReader(FileReader(file))
		val fileSize = file.length()
		var lineCount = 0
		var wordCount = 0
		while (true) {
			val line = bufferedReader.readLine() ?: break
			++lineCount
			wordCount += line.split(" ").size
		}
		bufferedReader.close()

		return "$lineCount $wordCount $fileSize $interpolatedArg"
	}

	override fun isValid(command: String) =
		command.split(" ").any { it != "" } && command.split(" ").first { it != "" } == "wc"

}