package commands

import bash.Environment
import java.io.InputStream
import java.io.OutputStream

class PwdCommand(args: List<String>) : ACommand("pwd", args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        output.write(System.getProperty("user.dir").toByteArray())
        output.write(10)    // \n ascii code
        return 0
    }
}

