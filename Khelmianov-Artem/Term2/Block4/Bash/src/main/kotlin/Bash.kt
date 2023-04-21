import bash.Environment
import bash.Interpreter
import bash.Lexer
import bash.Parser
import utils.ASTPrettyPrinter
import utils.BashException
import utils.ILogger
import utils.LexerPrettyPrinter
import java.io.InputStream
import java.io.OutputStream

class Bash(
    private val logger: ILogger
) {
    private val debug = false
    private val interpreter = Interpreter(Environment(), logger)
    fun main(
        inputStream: InputStream = System.`in`,
        outputStream: OutputStream = System.out,
        errorStream: OutputStream = System.err
    ): Int {
        inputStream.bufferedReader().use { input ->
            while (true) {
                if (inputStream == System.`in`) print("\n>>> ")
                val line = input.readLine() ?: break
                try {
                    val tokens = Lexer(line, logger).lex()
                        .also { if (debug) println(LexerPrettyPrinter(it)) }
                    val ast = Parser(tokens, logger).parse()
                        .also { if (debug) println(ASTPrettyPrinter().run(it)) }
                    val (exiting, exitCode, output, error) = interpreter.interpret(ast)

                    output.transferTo(outputStream)
                    error.transferTo(errorStream)
                    if (exiting) {
                        return exitCode
                    }
                } catch (_: BashException) {
//                    return 255
                }
            }
        }
        return 0
    }
}

