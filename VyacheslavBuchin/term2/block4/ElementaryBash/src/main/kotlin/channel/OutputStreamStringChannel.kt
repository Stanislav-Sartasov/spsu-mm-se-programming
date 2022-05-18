package channel

import java.io.OutputStream

class OutputStreamStringChannel(
	override val outputStream: OutputStream,
	val separator: String = System.lineSeparator()
) : Channel<String> {
	override val inputStream = "".byteInputStream()
	private val writer = outputStream.bufferedWriter()
	override fun write(value: String) {
		writer.apply {
			write(value)
			write(separator)
			flush()
		}
	}

	override fun read() = ""
}