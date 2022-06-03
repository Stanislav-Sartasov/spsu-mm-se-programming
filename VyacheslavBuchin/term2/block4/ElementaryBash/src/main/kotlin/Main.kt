import channel.InputStreamStringChannel
import channel.OutputStreamStringChannel
import interpreter.ElementaryBashInterpreter
import parser.ElementaryBashParser
import preprocessor.ElementaryBashPreprocessor
import service.substitution.MapSubstitutionManager
import tokenizer.ElementaryBashTokenizer
import util.ElementaryBashCommands

fun main() {
	val manager = MapSubstitutionManager()
	val preprocessor = ElementaryBashPreprocessor(manager)
	val tokenizer = ElementaryBashTokenizer()
	val parser = ElementaryBashParser()
	val commandManager = ElementaryBashCommands.getBuiltInCommandsManager(manager)

	val input = InputStreamStringChannel(System.`in`)
	val output = OutputStreamStringChannel(System.out)
	val error = OutputStreamStringChannel(System.err, "")
	val interpreter =  ElementaryBashInterpreter(commandManager, input, output, error)
	ElementaryBashApp(preprocessor, tokenizer, parser, interpreter).run()
}