import simpleBashInterpreter.SimpleBashInterpreter

object App {

	private const val WELCOME_MESSAGE =
		"Welcome to the simple version of the bash command shell.\nType \"help\" to get a list of commands.\n"

	private const val EXIT_MESSAGE =
		"Exiting the interpreter..."

	fun run() {
		val interpreter = SimpleBashInterpreter()

		println(WELCOME_MESSAGE)
		while (true) {
			print("> ")
			println(interpreter.interpret(readln(), "") ?: break)
		}
		println(EXIT_MESSAGE)
	}

}
