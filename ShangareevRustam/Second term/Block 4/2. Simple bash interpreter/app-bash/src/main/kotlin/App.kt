import simpleBashInterpreter.SimpleBashInterpreter

object App {

	private const val WELCOME_MESSAGE =
		"Welcome to the simple version of the bash command shell.\nType \"help\" to get a list of commands."

	private const val EXIT_MESSAGE =
		"Exiting the interpreter..."

	private val HELP_MESSAGE = StringBuilder()
		.append("| echo \"[string]\" - display a string, possibly containing predefined variables\n")
		.append("| exit - exit the interpreter\n")
		.append("| pwd - display the current working directory (name and list of files)\n")
		.append("| cat [FILENAME] - show the contents of a file on the screen\n")
		.append("| wc [FILENAME] - display the number of lines, words and bytes in a file\n")
		.append("| a=4 or a=\"3217 321 321\" - assigning a variable\n")
		.append("| A command not recognized as one of the above causes the" +
				" operating system mechanisms to try to start\n")
		.append("| \n")
		.append("| Operators: \n")
		.append("| operator \$ - assignment and use of local session variables (e.g. \$PATH, \$a)\n")
		.append("| operator | - pipelined command processing. The result of executing" +
				" one command becomes the entrance to another.").toString()


	fun run() {
		val interpreter = SimpleBashInterpreter()

		println(WELCOME_MESSAGE)
		println()
		while (true) {
			print("> ")
			when (val input = readln()) {
				"help" -> {
					println(HELP_MESSAGE)
				}
				"exit" -> {
					println(EXIT_MESSAGE)
					break
				}
				else -> {
					println(interpreter.interpret(input, ""))
				}
			}
		}
	}

}
