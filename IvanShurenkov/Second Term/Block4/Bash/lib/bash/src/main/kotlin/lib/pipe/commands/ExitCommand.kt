package lib.pipe.commands

import lib.pipe.Output

class ExitCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        return Output(null, null, true)
    }
}