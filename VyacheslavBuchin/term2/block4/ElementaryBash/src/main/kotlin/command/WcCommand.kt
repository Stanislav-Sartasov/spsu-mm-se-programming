package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel
import java.io.File

class WcCommand(
	override val input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override val output = StringChannel()
	override val error = StringChannel()


	override fun execute(args: Array<String>): Int {
		try {
			val text = extractText(args)
			output.write("${text.countLines()} ${text.countWords()} ${text.length}${System.lineSeparator()}")
		} catch (e: Exception) {
			error.write(e.message ?: "")
			return 1
		}
		return 0
	}

	private fun extractText(args: Array<String>): String {
		if (args.isEmpty())
			return input.read()

		File(args[0]).bufferedReader().use {
			return it.readText()
		}
	}

	private fun String.countLines() = this.trim().lines().count()

	private fun String.countWords() = this.trim().split("\\s".toRegex()).filter { it.isNotEmpty() }.size
}