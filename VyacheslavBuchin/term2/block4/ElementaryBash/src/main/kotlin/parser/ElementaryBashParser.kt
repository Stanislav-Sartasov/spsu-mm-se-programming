package parser

import exception.ElementaryBashException

class ElementaryBashParser : Parser {
	override fun parse(tokens: List<String>): List<List<String>> {
		if (tokens.isEmpty())
			return listOf()
		if (tokens.last() == "|")
			throw ElementaryBashException(ElementaryBashException.SYNTAX_ERROR, "pipe to nothing")
		if (tokens.first() == "|")
			throw ElementaryBashException(ElementaryBashException.SYNTAX_ERROR, "pipe from nothing")
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