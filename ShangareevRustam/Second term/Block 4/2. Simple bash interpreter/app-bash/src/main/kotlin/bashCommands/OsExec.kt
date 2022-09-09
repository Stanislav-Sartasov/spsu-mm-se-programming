package bashCommands

import java.io.BufferedReader
import java.io.InputStreamReader
import java.util.stream.Collectors

object OsExec : BashCommand() {

	override fun run(arg: String, input: String, arguments: MutableMap<String, String>): String? {
		try {
			val res = Runtime.getRuntime().exec("$arg $input")
			if (!res.isAlive) return BufferedReader(InputStreamReader(res.errorStream))
				.lines().collect(Collectors.joining("\n")).toString()
			return ""
		} catch (e: Throwable) {
			return "bash: $arg: command/program not found"
		}
	}

	override fun isValid(command: String) = command.split(" ").any { it != "" }

}