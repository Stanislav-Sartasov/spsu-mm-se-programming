package simpleBashInterpreter

object SimpleBashTextProcessor {

	fun preprocessCommand(command: String, arguments: Map<String, String>): List<String> {
		val unprocessedCommand = if (command.isNotEmpty() && command.first() == '"' && command.last() == '"') {
			command.slice(1 until command.length - 1)
		} else {
			command
		}
		val result = StringBuilder()
		val strLen = unprocessedCommand.length

		var ptr = 0
		while (ptr < strLen) {
			if (unprocessedCommand[ptr] == '$') {
				val curVar = StringBuilder("$")
				++ptr
				while (ptr < strLen && (unprocessedCommand[ptr].isLetterOrDigit() || unprocessedCommand[ptr] == '_')) {
					curVar.append(unprocessedCommand[ptr])
					++ptr
				}

				if (curVar.toString() in arguments) result.append(arguments[curVar.toString()])
				else result.append("")
			} else {
				result.append(unprocessedCommand[ptr])
				++ptr
			}
		}

		return result.split(" | ")
	}

}