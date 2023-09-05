package commands

import bash.Environment
import java.io.File
import java.io.InputStream
import java.io.OutputStream

class CatCommand(args: List<String>) : ACommand("cat", args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        var exitCode = 0
        if (args.isEmpty()) {
            input.copyTo(output)
        } else {
            for (file in args) {
                try {
                    output.write(File(file).readBytes())
                } catch (e: Exception) {
                    error.write("[cat] Error while reading file $file: $e\n".toByteArray())
                    exitCode = 1
                }
            }
        }
        return exitCode
    }
}
