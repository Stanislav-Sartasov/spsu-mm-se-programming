package preprocessor

import service.substitution.SubstitutionManager

class ElementaryBashPreprocessor(
	private val substitutionManager: SubstitutionManager
) : Preprocessor {

	private val unexpectedEOF = "syntax error: unexpected end of file"

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
					if (index >= str.length)
						break
					if (!str[index].isLetter() && str[index] != '_' && str[index] != '{') {
						builder.append('$')
						builder.append(str[index])
						index++
						continue
					}
					if (str[index] == '"' || str[index] == '\'') {
						continue
					}
					if (str[index] == '{') {
						val closedInd = str.indexOf('}', index)
						if (closedInd == -1)
							throw PreprocessorException(unexpectedEOF)
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
			throw PreprocessorException(unexpectedEOF)
		return builder.toString()
	}

	private fun shouldStop(c: Char) = c.isWhitespace() || c == '"' || c == '\''
}