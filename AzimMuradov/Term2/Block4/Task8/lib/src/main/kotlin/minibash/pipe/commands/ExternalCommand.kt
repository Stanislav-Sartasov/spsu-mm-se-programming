package minibash.pipe.commands

import kotlinx.coroutines.*
import kotlinx.coroutines.flow.Flow
import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.FlowUtils.joinToString
import minibash.utils.FlowUtils.toCharFlow
import java.io.File

class ExternalCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Flow<Char>?): CommandRunOut {
        val process = ProcessBuilder(name, *args.toTypedArray()).apply {
            runBlocking {
                redirectInput(
                    withContext(Dispatchers.IO) {
                        File.createTempFile(name.padStart(length = 3, padChar = '-'), "minibash")
                    }.apply {
                        if (input != null) writeText(input.joinToString())
                    }
                )
            }
        }.start()

        return CommandRunOut(
            output = process.inputReader().readText().toCharFlow(),
            errors = process.errorReader().readText().toCharFlow(),
        )
    }
}
