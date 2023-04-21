import lib.interpreter.Interpreter
import lib.pipe.Output
import lib.pipe.commands.*
import org.kodein.di.DI
import org.kodein.di.bindSingleton


fun main(args: Array<String>) {
    val commands = DI {
        bindSingleton<ICommand>("cat") { CatCommand("cat") }
        bindSingleton<ICommand>("echo") { EchoCommand("echo") }
        bindSingleton<ICommand>("pwd") { PwdCommand("pwd") }
        bindSingleton<ICommand>("wc") { WcCommand("wc") }
        bindSingleton<ICommand>("exit") { ExitCommand("exit") }
    }
    var output = Output()
    val interpreter = Interpreter()
    while (!output.exit) {
        print(">")
        val line = readLine()
        output = Output()
        if (line != null) {
            output = interpreter.run(line, commands)
            if (output.error != null && output.error != "")
                print("Error: ${output.error}")
            if (output.output != null)
                print(output.output)
        }
    }
}
