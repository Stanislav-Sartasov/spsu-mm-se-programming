package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel
import exception.ElementaryBashException
import java.io.File
import java.io.IOException

class CatCommand(
	override var input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override var output: Channel<String> = StringChannel("")
	override var error: Channel<String> = StringChannel()

	override fun execute(args: Array<String>): Int {
		if (args.size > 1)
			throw ElementaryBashException(ElementaryBashException.INVALID_ARGUMENTS, "too many arguments")
		try {
			val text = extractText(args)
			output.write(text)
		} catch (e: IOException) {
			throw ElementaryBashException(
				ElementaryBashException.INVALID_ARGUMENTS,
				e.message?.lowercase() ?: ElementaryBashException.UNKNOWN_ERROR
			)
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