package simpleBashInterpreter

import simpleBashInterpreter.SimpleBashTextProcessor.interpolateStringWithVariables
import simpleBashInterpreter.SimpleBashTextProcessor.isAssignment
import simpleBashInterpreter.SimpleBashTextProcessor.isCat
import simpleBashInterpreter.SimpleBashTextProcessor.isEcho
import simpleBashInterpreter.SimpleBashTextProcessor.isInterpolated
import simpleBashInterpreter.SimpleBashTextProcessor.isWc
import java.io.BufferedReader
import java.io.File
import java.io.FileReader
import java.io.InputStreamReader
import java.util.stream.Collectors

class SimpleBashInterpreter {

	private val arguments = mutableMapOf<String, String>()

	fun interpret(command: String, input: String): String? {

		if (command == "pwd") return pwd("", input)
		else if (command == "exit") return  null
		else if (isEcho(command)) return echo(command, input)
		else if (isCat(command)) return  cat(command, input)
		else if (isWc(command)) return wc(command, input)
		else if (isAssignment(command)) return performAssignment(command, input)
		else if (command.first() == '$' || isAssignment(command.drop(1))) return performAssignment(command.drop(1), input)
		else if (command.indexOf(" | ") != -1) return pipeInterpret(command)
		else if (isInterpolated(command, arguments)) return interpret(interpolateStringWithVariables(command, arguments), input)

		return osExec(command, input)
	}

	private fun pipeInterpret(command: String): String {
		val listOfCommands = command.split(" | ")

		var result = ""
		for (instruction in listOfCommands) {
			result = interpret(instruction, result) ?: ""
		}

		return result
	}

	private fun performAssignment(arg: String, input: String): String {
		val argument = arg.slice(indices = 0 until arg.indexOf('='))
		val value = arg.slice(indices = arg.indexOf('=') + 1 until arg.length)

		if (argument.isNotEmpty()) {
			arguments["\$$argument"] = interpolateStringWithVariables(value, arguments)
		}
		return ""
	}

	private fun osExec(arg: String, input: String): String {
		try {
			val res = Runtime.getRuntime().exec("$arg $input")
			if (!res.isAlive) return BufferedReader(InputStreamReader(res.errorStream))
					.lines().collect(Collectors.joining("\n")).toString()
			return ""
		} catch (e: Throwable) {
			return "bash: $arg: command/program not found"
		}
	}

	private fun echo(arg: String, input: String): String {
		return interpolateStringWithVariables(
			arg.slice(5 until arg.length),
			arguments
		)
	}

	private fun cat(arg: String, input: String): String {
		val interpolatedArg = interpolateStringWithVariables(
			arg.slice(4 until arg.length),
			arguments
		)
		if (interpolatedArg == "") return input

		val file = File(interpolatedArg)
		if (!file.exists()) return "cat: $interpolatedArg: No such file or directory"
		if (file.isDirectory) return "cat: $interpolatedArg: Is a directory"

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
			return "cat: $interpolatedArg: An error occurred while reading the file"
		}
		return result.toString()
	}

	private fun pwd(arg: String, input: String): String {
		return System.getProperty("user.dir")
	}

	private fun wc(arg: String, input: String): String {
		val interpolatedArg = interpolateStringWithVariables(
			arg.slice(3 until arg.length),
			arguments
		)
		if (interpolatedArg == "") return "1 ${input.split(" ").size} ${input.length}"

		val file = File(interpolatedArg)
		if (!file.exists()) return "wc: $interpolatedArg: No such file or directory"
		if (file.isDirectory) return "wc: $interpolatedArg: Is a directory\n 0 0 0 $interpolatedArg"

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

		return "$lineCount $wordCount $fileSize $interpolatedArg"
	}

	
}
