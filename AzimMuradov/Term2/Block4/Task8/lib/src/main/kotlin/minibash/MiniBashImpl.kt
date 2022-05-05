package minibash

import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import minibash.interpretation.Interpreter
import minibash.parsing.Parser
import minibash.pipe.Signal
import java.io.InputStream
import java.io.PrintStream

class MiniBashImpl(
    private val parser: Parser,
    private val interpreter: Interpreter,
) : MiniBash {

    override suspend fun run(
        inputStream: InputStream,
        outputStream: PrintStream,
        errorsStream: PrintStream,
    ) {
        val variables = mutableMapOf<String, String>()

        inputStream.use { `is` ->
            outputStream.use { os ->
                errorsStream.use { es ->
                    while (true) {
                        val instruction = withContext(Dispatchers.IO) {
                            `is`.reader().buffered().readLine()
                        }?.let(parser::parse) ?: break

                        val (variable, output, errors, signal) = interpreter.interpret(instruction, variables)

                        if (variable != null) variables += variable

                        output?.collect(os::print)
                        errors?.collect(es::print)

                        when (signal) {
                            null -> Unit
                            Signal.SIGINT -> break
                        }
                    }
                }
            }
        }
    }
}
