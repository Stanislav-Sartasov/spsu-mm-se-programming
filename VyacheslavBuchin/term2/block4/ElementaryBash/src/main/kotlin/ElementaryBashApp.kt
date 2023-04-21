import exception.ElementaryBashException
import interpreter.Interpreter
import parser.ElementaryBashParser
import parser.Parser
import preprocessor.ElementaryBashPreprocessor
import preprocessor.Preprocessor
import tokenizer.ElementaryBashTokenizer
import tokenizer.Tokenizer
import java.io.BufferedReader
import java.io.InputStreamReader

class ElementaryBashApp(
	private val preprocessor: Preprocessor = ElementaryBashPreprocessor(),
	private val tokenizer: Tokenizer = ElementaryBashTokenizer(),
	private val parser: Parser = ElementaryBashParser(),
	private val interpreter: Interpreter
) {
	private val console = BufferedReader(InputStreamReader(System.`in`))
	private val name = "ElBASH"
	private var lastCode: Int? = null
	private val greetings =
"""
	Hey there! It's Elementary Bash (ElBASH for simple)
	I'm bash-like interpreter!
	Functionality with semantics similar to bash:
	Commands and pipes (built-in are pwd, cat, wc, echo, exit)
	Single and double quotes
	Substitutions (for assignment VAR_NAME=VALUE, for substitution ${"\$VAR_NAME"} is used
	Quotes, pipes, spaces can be escaped with \ before them (e. g. echo message\| another_message)
	If built-in or OS command should have an input (like wc or cat without arguments) and there's no pipe into it
	you will be prompted to enter from the console
	The square brackets of the input prompt display the exit code of the last executed command
""".trimIndent()

	fun run() {
		println(greetings)
		while (true) {
			try {
				print("$name${if (lastCode != null) "[$lastCode]" else ""}> ")
				var s = console.readLine() ?: break
				s = preprocessor.applySubstitutions(s)
				val tokens = tokenizer.tokenize(s)
				lastCode = interpreter.interpret(parser.parse(tokens))
			} catch (exception: ElementaryBashException) {
				lastCode = 1
				println(
					"${exception.name}: ${exception.message}"
				)
			}
		}
	}
}