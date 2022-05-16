package tokenizer

class ElementaryBashTokenizer : Tokenizer {

	private val builder = StringBuilder()

	override fun tokenize(str: String): List<String> {
		val result = mutableListOf<String>()
		builder.clear()
		var index = -1
		var isInDoubleQuotes = false
		var isInSingleQuotes = false
		var isLastBackslash = false
		while (index + 1 < str.length) {
			index++
			val c = str[index]
			if (c == '\\') {
				isLastBackslash = true
				continue
			}
			if (c == '"' && !isInSingleQuotes && !isLastBackslash) {
				isInDoubleQuotes = !isInDoubleQuotes
				continue
			}
			if (c == '\'' && !isInDoubleQuotes && !isLastBackslash) {
				isInSingleQuotes = !isInSingleQuotes
				continue
			}
			if ((c == ' ' || c == '|' || c == '"' || c == '\'') && isLastBackslash) {
				builder.append(c)
				isLastBackslash = false
				continue
			}
			if (isInDoubleQuotes || isInSingleQuotes) {
				builder.append(c)
				continue
			}
			if (isLastBackslash) {
				isLastBackslash = false
				builder.append('\\')
			}
			if (c == '|') {
				result.add(builder.toString())
				builder.clear()
				result.add("|")
				continue
			}
			if (!c.isWhitespace()) {
				builder.append(c)
				continue
			}
			result.add(builder.toString())
			builder.clear()
		}
		result.add(builder.toString())
		return result.filter { it.isNotEmpty() }
	}
}