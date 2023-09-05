package channel

import java.io.InputStream
import java.io.OutputStream

class InputStreamStringChannel(override val inputStream: InputStream) : Channel<String> {
	override val outputStream: OutputStream = OutputStream.nullOutputStream()
	private val bufferedReader = inputStream.bufferedReader()
	override fun write(value: String) { }

	override fun read() = bufferedReader.readText()
}