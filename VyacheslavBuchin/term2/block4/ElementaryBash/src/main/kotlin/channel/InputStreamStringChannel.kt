package channel

import java.io.InputStream

class InputStreamStringChannel(inputStream: InputStream) : Channel<String> {
	private val bufferedReader = inputStream.bufferedReader()
	override fun write(value: String) { }

	override fun read() = bufferedReader.readText()
}