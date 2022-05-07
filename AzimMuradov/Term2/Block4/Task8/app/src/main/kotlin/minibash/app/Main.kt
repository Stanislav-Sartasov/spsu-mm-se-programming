package minibash.app

import minibash.MiniBashImpl
import minibash.interpretation.InterpreterImpl
import minibash.parsing.SimpleParser
import minibash.pipe.commands.*
import minibash.utils.StringUtils.createErrorMessage
import java.io.File

fun main(args: Array<String>) {
    val echo = EchoCommand(name = "echo")
    val exit = ExitCommand(name = "exit")
    val pwd = PwdCommand(name = "pwd")
    val cat = CatCommand(name = "cat")
    val wc = WcCommand(name = "wc")

    val miniBash = MiniBashImpl(
        parser = SimpleParser,
        interpreter = InterpreterImpl(availableCommands = listOf(echo, exit, pwd, cat, wc))
    )


    when (args.size) {
        0 -> {
            try {
                miniBash.run(System.`in`, System.out, System.err)
            } catch (e: Throwable) {
                System.err.println(createErrorMessage(cmd = "minibash", e = e))
                println(hintForHelp())
            }
        }
        1 -> {
            val arg = args.first()
            if (arg.startsWith(prefix = "--")) {
                when (arg) {
                    "--help" -> println(helpMessage())
                    "--version" -> println(VERSION)
                    else -> {
                        System.err.println("minibash: unknown option '$arg'")
                        println(hintForHelp())
                    }
                }
            } else {
                try {
                    miniBash.run(File(arg).inputStream(), System.out, System.err)
                } catch (e: Throwable) {
                    System.err.println(createErrorMessage(cmd = "minibash", e = e))
                    println(hintForHelp())
                }
            }
        }
        else -> {
            System.err.println("minibash: too many arguments")
            println(hintForHelp())
        }
    }
}


const val VERSION = "1.0.0"

fun helpMessage() =
    """
        |Mini bash, version $VERSION
        |Usage:
        |   minibash
        |   minibash [OPTION]
        |   minibash [SCRIPT_FILE]
        |Options:
        |   --help            - show current message
        |   --version         - show minibash version
        |Commands:
        |   echo              - write arguments to standard output
        |   exit              - cause the shell to exit
        |   pwd               - return working directory name and list all its files
        |   cat [FILENAME...] - concatenate and print files
        |   wc [FILENAME...]  - print newline, word, and byte counts for each file
        """.trimMargin().replace(regex = """\R""".toRegex(), replacement = System.lineSeparator())

fun hintForHelp() = """To show help message run `minibash --help`"""
