package simpleBashInterpreter

import java.io.BufferedReader
import java.io.File
import java.io.FileReader
import java.io.InputStreamReader
import java.util.stream.Collectors

class SimpleBashInterpreter {

	private companion object {
		private const val NO_SUCH_FILE_MESSAGE =
			"No such file or directory"

		private const val DIRECTORY_FILE_MESSAGE =
			"Is a directory"

		private const val ERROR_WHILE_READING_MESSAGE =
			"An error occurred while reading the file"

		private val arguments = mutableMapOf<String, String>()
	}

	fun interpret(command: String, stdin: String): String {

		if (command == "pwd") {
			return pwd("", stdin)
		}
		else if (command == "exit") {
			return ""
		}
		else if (command.take(4) == "echo" && isValuable(command.slice(5 until command.length))) {
			return echo(interpolateStringWithVariables(command.slice(5 until command.length)), stdin)
		}
		else if (command.take(3) == "cat" && isValuable(command.slice(4 until command.length))) {
			return cat(interpolateStringWithVariables(command.slice(4 until command.length)), stdin)
		}
		else if (command.take(2) == "wc" && isValuable(command.slice(3 until command.length))) {
			return wc(interpolateStringWithVariables(command.slice(3 until command.length)), stdin)
		}
		else if (isAssignment(command)) {
			performAssignment(command, stdin)
			return ""
		}
		else if (command.indexOf(" | ") != -1) {
			return pipeInterpret(command)
		}
		else if (command.first() == '$') {
			return interpret(interpolateStringWithVariables(command), stdin)
		}

		return exec(command, stdin)
	}

	private fun pipeInterpret(command: String): String {
		val listOfCommands = command.split(" | ")

		var result = ""
		for (instruction in listOfCommands) {
			result = interpret(instruction, result)
		}

		return result
	}

	private fun isAssignment(command: String) =
		command.indexOf('=') != -1 &&
				command.slice(0 until command.indexOf('=')).all { isCorrectChar(it) } &&
				isValuable(command.slice(command.indexOf('=') + 1 until command.length))

	private fun isValuable(value: String) =
		value.isEmpty() || value.first() == '"' && value.last() == '"' || value.indexOf(' ') == -1

	private fun isCorrectChar(symb: Char) =
		(symb in 'A'..'Z' || symb in 'a'..'z' || symb in '0'..'9' || symb == '_')

	private fun performAssignment(input: String, stdin: String) {
		val argument = input.slice(
			indices = 0 until input.indexOf('=')
		)
		val value = input.slice(
			indices = input.indexOf('=') + 1 until input.length
		)

		if (argument.isNotEmpty()) {
			arguments["\$$argument"] = interpolateStringWithVariables(value)
		}
	}

	private fun exec(input: String, stdin: String): String {
		try {
			val res = Runtime.getRuntime().exec("$input $stdin")
			if (!res.isAlive) return BufferedReader(InputStreamReader(res.errorStream))
					.lines().collect(Collectors.joining("\n")).toString()
			return ""
		} catch (e: Throwable) {
			return "bash: $input: ${e.message}"
		}
	}

	private fun echo(input: String, stdin: String): String {
		return interpolateStringWithVariables(input)
	}

	private fun cat(input: String, stdin: String): String {
		if (input == "") return stdin

		val file = File(input)
		if (!file.exists()) return "cat: $input: $NO_SUCH_FILE_MESSAGE"
		if (file.isDirectory) return "cat: $input: $DIRECTORY_FILE_MESSAGE"

		val result = StringBuilder()
		try {
			val bufferedReader = BufferedReader(FileReader(file))
			while (true) {
				val line = bufferedReader.readLine()
				result.append(if (line == null) break else "$line\n")
			}
			bufferedReader.close()
		}
		catch (e: Exception) {
			return "cat: $input: $ERROR_WHILE_READING_MESSAGE"
		}
		return result.toString()
	}

	private fun pwd(input: String, stdin: String): String {
		return System.getProperty("user.dir")
	}

	private fun wc(input: String, stdin: String): String {
		if (input == "") return "1 ${stdin.split(" ").size} ${stdin.length}"

		val file = File(input)
		if (!file.exists()) return "wc: $input: $NO_SUCH_FILE_MESSAGE"
		if (file.isDirectory) return "wc: $input: $DIRECTORY_FILE_MESSAGE\n 0 0 0 $input"

		val bufferedReader = BufferedReader(FileReader(file))
		val fileSize = file.length()
		var lineCount = 0
		var wordCount = 0
		while (true) {
			val line = bufferedReader.readLine() ?: break
			++lineCount
			wordCount += line.split(" ").size
		}
		bufferedReader.close()

		return "$lineCount $wordCount $fileSize $input"
	}

	private fun interpolateStringWithVariables(string: String): String {
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

				if (curVar.toString() in arguments) result.append(arguments[curVar.toString()])
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
