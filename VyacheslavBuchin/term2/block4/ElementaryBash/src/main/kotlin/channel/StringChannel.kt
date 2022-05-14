package channel

class StringChannel(val separator: String = System.lineSeparator()) : Channel<String> {

	companion object {
		fun nullChannel() = object : Channel<String> {
			override fun read() = ""
			override fun write(value: String) { }
		}
	}

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
