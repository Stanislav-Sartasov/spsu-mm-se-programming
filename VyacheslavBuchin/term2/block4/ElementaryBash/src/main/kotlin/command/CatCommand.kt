package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel
import java.io.File

class CatCommand(
	override val input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override val output = StringChannel("")
	override val error = StringChannel()

	override fun execute(args: Array<String>): Int {
		try {
			val text = extractText(args)
			output.write(text)
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

}