package minibash

import minibash.interpretation.Interpreter
import minibash.parsing.Parser
import minibash.pipe.Signal
import java.io.InputStream
import java.io.PrintStream

class MiniBashImpl(
    private val parser: Parser,
    private val interpreter: Interpreter,
) : MiniBash {

    override fun run(
        inputStream: InputStream,
        outputStream: PrintStream,
        errorsStream: PrintStream,
    ) {
        val variables = mutableMapOf<String, String>()

        inputStream.use { `is` ->
            outputStream.use { os ->
                errorsStream.use { es ->
                    for (line in `is`.bufferedReader().lineSequence()) {
                        val instruction = parser.parse(line)

                        val (variable, output, errors, signal) = interpreter.interpret(instruction, variables)

                        if (variable != null) variables += variable

                        output?.forEach(os::print)
                        errors?.forEach(es::print)

                        if (signal == Signal.SIGINT) break
                    }
                }
            }
        }
    }
}
