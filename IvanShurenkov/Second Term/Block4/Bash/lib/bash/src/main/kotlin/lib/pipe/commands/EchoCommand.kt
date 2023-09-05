package lib.pipe.commands

import lib.pipe.Output

class EchoCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        return Output("${args.joinToString(separator = " ")}${System.lineSeparator()}")
    }
}