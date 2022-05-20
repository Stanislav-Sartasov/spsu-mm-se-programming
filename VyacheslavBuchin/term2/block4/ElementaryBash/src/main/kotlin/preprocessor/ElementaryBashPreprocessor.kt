package preprocessor

import exception.ElementaryBashException
import service.substitution.SubstitutionManager

class ElementaryBashPreprocessor(
	private val substitutionManager: SubstitutionManager
) : Preprocessor {

	override fun applySubstitutions(str: String): String {
		val builder = StringBuilder()
		var index = 0
		var isInQuotes = false
		var isInDoubleQuotes = false
		var isLastBackSlash = false
		while (index < str.length) {
			if (str[index] == '\\') {
				isLastBackSlash = true
				builder.append('\\')
				index++
				continue
			}
			when (val char = str[index]) {
				'\'' -> {
					if (!isLastBackSlash && !isInDoubleQuotes) {
						isInQuotes = !isInQuotes
					}
					builder.append(char)
				}
				'"' -> {
					if (!isLastBackSlash && !isInQuotes) {
						isInDoubleQuotes = !isInDoubleQuotes
					}
					builder.append(char)
				}
				'$' -> {
					if (isInQuotes) {
						builder.append('$')
						index++
						continue
					}
					index++
					if (index >= str.length) {
						builder.append('$')
						break
					}
					if (!str[index].isLetter() && str[index] != '_' && str[index] != '{') {
						builder.append('$')
						continue
					}
					if (str[index] == '"' || str[index] == '\'') {
						continue
					}
					if (str[index] == '{') {
						val closedInd = str.indexOf('}', index)
						if (closedInd == -1)
							throw ElementaryBashException(
								ElementaryBashException.SYNTAX_ERROR,
								"substitution curly braces was opened but wasn't closed"
							)
						builder.append(
							substitutionManager[str.substring(index + 1, closedInd)]
						)
						index = closedInd
					} else {
						val substitutionBuilder = StringBuilder()
						while (index < str.length && !shouldStop(str[index]) && str[index] != '|') {
							substitutionBuilder.append(str[index])
							index++
						}
						builder.append(
							substitutionManager[substitutionBuilder.toString()]
						)
						index--
					}
				}
				else -> {
					builder.append(char)
				}
			}
			isLastBackSlash = false
			index++
		}
		if (isInQuotes || isInDoubleQuotes)
			throw ElementaryBashException(ElementaryBashException.SYNTAX_ERROR, "quotes was opened but wasn't closed")
		return builder.toString()
	}

	private fun shouldStop(c: Char) = c.isWhitespace() || c == '"' || c == '\''
}