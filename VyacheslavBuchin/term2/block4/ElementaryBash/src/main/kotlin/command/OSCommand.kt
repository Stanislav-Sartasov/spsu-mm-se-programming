package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel
import exception.ElementaryBashException
import java.io.IOException

class OSCommand(
	private val name: String,
	override var input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override var error: Channel<String> = StringChannel()
	override var output: Channel<String> = StringChannel("")

	override fun execute(args: Array<String>): Int {
		val inputStream = input.inputStream
		val commandsAndArgs = mutableListOf(name).apply { addAll(args) }
		val processBuilder = ProcessBuilder(commandsAndArgs)

		if (inputStream == System.`in`)
			processBuilder.redirectInput(ProcessBuilder.Redirect.INHERIT)

		try {
			val process = processBuilder.redirectError(ProcessBuilder.Redirect.INHERIT).start()
			if (inputStream != System.`in`) {
				try {
					process.outputStream.bufferedWriter().use {
						it.write(inputStream.bufferedReader().readText())
					}
				} catch (ignored: IOException) { }
			}
			process.waitFor()
			process.inputStream.bufferedReader().use {
				output.write(it.readText())
			}
//			process.errorStream.bufferedReader().use {
//				error.write(it.readText())
//			}
			return process.exitValue()
		} catch (exception: IOException) {
			throw ElementaryBashException(ElementaryBashException.UNKNOWN_COMMAND, name)
		}
	}

}