package parser

class ElementaryBashParser : Parser {
	override fun parse(tokens: List<String>): List<List<String>> {
		if (tokens.last() == "|")
			throw ParseException("syntax error: unexpected end of file")
		val result = mutableListOf<MutableList<String>>()
		var index = 0
		while (index < tokens.size) {
			result.add(mutableListOf())
			while (index < tokens.size && tokens[index] != "|") {
				result.last().add(tokens[index])
				index++
			}
			index++
		}
		return result
	}
}