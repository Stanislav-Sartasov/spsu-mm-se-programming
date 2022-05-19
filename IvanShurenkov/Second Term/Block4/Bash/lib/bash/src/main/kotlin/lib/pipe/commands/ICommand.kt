package lib.pipe.commands

import lib.pipe.Output

interface ICommand {
    fun run(args: Array<String>, input: String? = null): Output
}