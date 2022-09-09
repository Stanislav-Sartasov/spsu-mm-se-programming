package commands

import bash.Environment
import utils.BashExitException
import java.io.InputStream
import java.io.OutputStream

class ExitCommand(args: List<String>) : ACommand("exit", args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        output.write("Exiting...".toByteArray())
        val exitCode = args.getOrNull(0)?.toIntOrNull() ?: env.exitCode
        throw BashExitException(exitCode)
    }
}
