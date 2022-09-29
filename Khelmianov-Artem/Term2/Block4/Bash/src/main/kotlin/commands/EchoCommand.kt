package commands

import bash.Environment
import java.io.InputStream
import java.io.OutputStream

class EchoCommand(args: List<String>) : ACommand("echo", args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        output.write(args.joinToString(" ", postfix = System.lineSeparator()).toByteArray())
        return 0
    }
}

