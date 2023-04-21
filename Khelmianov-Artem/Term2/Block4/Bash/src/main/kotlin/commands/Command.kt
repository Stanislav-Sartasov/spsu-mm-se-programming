package commands

import bash.Environment
import java.io.InputStream
import java.io.OutputStream

abstract class ACommand(val cmd: String, val args: List<String>) {
    abstract fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int
}


