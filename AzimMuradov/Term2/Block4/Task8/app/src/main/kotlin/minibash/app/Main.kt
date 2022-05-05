package minibash.app

import minibash.MiniBashImpl
import minibash.interpretation.InterpreterImpl
import minibash.parsing.SimpleParser
import minibash.pipe.commands.*

fun main(args: Array<String>) {
    when (args.size) {
        0 -> {
            val echo = EchoCommand(name = "echo")
            val exit = ExitCommand(name = "exit")
            val pwd = PwdCommand(name = "pwd")
            val cat = CatCommand(name = "cat")
            val wc = WcCommand(name = "wc")

            val miniBash = MiniBashImpl(
                parser = SimpleParser,
                interpreter = InterpreterImpl(availableCommands = listOf(echo, exit, pwd, cat, wc))
            )

            miniBash.run(System.`in`, System.out, System.err)
        }
        1 -> {
            // TODO
        }
        else -> {
            System.err.println("minibash: too many arguments")
        }
    }
}
