package lib.pipe.commands

import lib.pipe.Output
import java.io.File

class WcCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        val output = Output(null)
        for (arg in args) {
            try {
                val text = File(arg).readText()
                val countLines = text.count { it == '\n' }
                val countWords = Regex("""\s+""").findAll(text.trim()).count() + 1
                val countBytes = text.toString().toByteArray().count()
                output.addOut("$countLines $countWords $countBytes $arg")
            } catch (e: Exception) {
                //print(e.message)
                output.addErr(e.message)
            }
        }
        return output
    }
}