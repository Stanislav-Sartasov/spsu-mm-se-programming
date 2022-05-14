package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel

class OSCommand(
	private val name: String,
	override val input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override val error = StringChannel()
	override val output = StringChannel()

	override fun execute(args: Array<String>): Int {
		val process = Runtime.getRuntime().exec(name)
		process.outputStream.bufferedWriter().use {
			it.write(input.read())
		}
		process.waitFor()
		process.inputStream.bufferedReader().use {
			output.write(it.readText())
		}
		process.errorStream.bufferedReader().use {
			error.write(it.readText())
		}
		return process.exitValue()
	}
}