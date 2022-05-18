package channel

import java.io.InputStream
import java.io.OutputStream

class StringChannel(val separator: String = System.lineSeparator()) : Channel<String> {
	companion object {
		fun nullChannel() = object : Channel<String> {
			override val inputStream = "".byteInputStream()
			override val outputStream = OutputStream.nullOutputStream()
			override fun read() = ""
			override fun write(value: String) { }
		}
	}

	override val inputStream: InputStream
		get() = read().byteInputStream()
	override val outputStream: OutputStream = OutputStream.nullOutputStream()

	private val stringBuilder = StringBuilder()

	override fun write(value: String) {
		stringBuilder.apply {
			append(value)
			append(separator)
		}
	}

	override fun read(): String {
		val result = stringBuilder.toString()
		stringBuilder.clear()
		return result
	}
}
