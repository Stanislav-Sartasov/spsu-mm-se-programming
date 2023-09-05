package lib.pipe.commands

import lib.pipe.Output
import java.io.File

class CatCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        val output = Output(null)
        for (arg in args) {
            try {
                val text = File(arg).readText()
                output.addOut("$text")
            } catch (e: Exception) {
                //print(e.message)
                output.addErr(e.message)
            }
        }
        return output
    }
}