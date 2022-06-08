package simpleBashInterpreter

object SimpleBashTextProcessor {

	private fun isCorrectChar(symb: Char) =
		(symb in 'A'..'Z' || symb in 'a'..'z' || symb in '0'..'9' || symb == '_')

	private fun isValuable(value: String) =
		value.isEmpty() || value.first() == '"' && value.last() == '"' || value.indexOf(' ') == -1

	fun isInterpolated(command: String, arguments: MutableMap<String, String>) =
		command.first() == '$' && command != interpolateStringWithVariables(command, arguments)

	fun isAssignment(command: String) =
		command.indexOf('=') != -1 &&
				command.slice(0 until command.indexOf('=')).all { isCorrectChar(it) } &&
				isValuable(command.slice(command.indexOf('=') + 1 until command.length))

	fun isEcho(command: String) =
		command.take(4) == "echo" && isValuable(command.slice(5 until command.length))

	fun isCat(command: String) =
		command.take(3) == "cat" && isValuable(command.slice(4 until command.length))

	fun isWc(command: String) =
		command.take(2) == "wc" && isValuable(command.slice(3 until command.length))

	fun interpolateStringWithVariables(string: String, arguments: MutableMap<String, String>): String {
		val result = StringBuilder()
		val strLen = string.length

		var ptr = 0
		while (ptr < strLen) {
			if (string[ptr] == '$') {
				val curVar = StringBuilder("$")
				++ptr
				while (ptr < strLen && isCorrectChar(string[ptr])) {
					curVar.append(string[ptr])
					++ptr
				}

				if (curVar.toString() in arguments)
					result.append(arguments[curVar.toString()])
				else result.append(curVar)
			} else {
				result.append(string[ptr])
				++ptr
			}
			continue
		}

		return if (result.isEmpty()) {
			return ""
		}
		else if (result.first() == '"' && result.last() == '"') {
			result.slice(1 until result.length - 1).toString()
		} else {
			result.toString()
		}
	}

}