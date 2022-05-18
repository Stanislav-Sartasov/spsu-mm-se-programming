package command

import channel.Channel
import channel.InputStreamStringChannel
import channel.StringChannel
import java.io.InputStream
import java.io.PrintStream

class OSCommand(
	private val name: String,
	override var input: Channel<String> = InputStreamStringChannel(System.`in`)
) : Command {
	override var error: Channel<String> = StringChannel()
	override var output: Channel<String> = StringChannel()

	private lateinit var oldIn: InputStream
	private lateinit var oldOut: PrintStream
	private lateinit var oldErr: PrintStream

	override fun execute(args: Array<String>): Int {
		takeIO()
		val processBuilder = ProcessBuilder(mutableListOf(name).apply { addAll(args) })
		val process = processBuilder.inheritIO().start()
		process.waitFor()
		releaseIO()
		return process.exitValue()
	}

	private fun takeIO() {
		oldIn = System.`in`
		System.setIn(input.inputStream)

		oldOut = System.out
		System.setOut(PrintStream(output.outputStream))

		oldErr = System.err
		System.setErr(PrintStream(error.outputStream))
	}

	private fun releaseIO() {
		System.setIn(oldIn)
		System.setOut(oldOut)
		System.setErr(oldErr)
	}
}