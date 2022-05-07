package minibash.pipe.commands

import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.StringUtils
import java.io.File

class ExternalCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Sequence<Char>?): CommandRunOut {
        val processBuilder = ProcessBuilder(name, *args.toTypedArray())

        processBuilder.redirectInput(
            File.createTempFile(name.padStart(length = 3, padChar = '-'), "minibash").apply {
                if (input != null) writeText(input.joinToString(separator = ""))
            }
        )

        return try {
            val process = processBuilder.start()

            CommandRunOut(
                output = process.inputReader().readText().asSequence(),
                errors = process.errorReader().readText().asSequence(),
            )
        } catch (e: Throwable) {
            CommandRunOut(errors = StringUtils.createErrorMessage(name, e).asSequence())
        }
    }
}
