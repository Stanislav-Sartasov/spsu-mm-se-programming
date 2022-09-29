package commands

import bash.Environment
import java.io.File
import java.io.InputStream
import java.io.OutputStream

class WcCommand(args: List<String>) : ACommand("wc", args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        var exitCode = 0
        var sumLines = 0
        var sumWords = 0
        var sumBytes = 0

        if (args.isEmpty()) {
            val (l, w, b) = count(input.bufferedReader().readText())
            output.write("$l\t$w\t$b".toByteArray())
        } else {
            for (file in args) {
                try {
                    val (l, w, b) = count(File(file).readText())
                    sumLines += l
                    sumWords += w
                    sumBytes += b
                    output.write("$l\t$w\t$b\t$file\n".toByteArray())
                } catch (e: Exception) {
                    exitCode = 1
                    error.write("[wc] Error while reading file $file: $e\n".toByteArray())
                }
            }
            if (args.size > 1) {
                output.write("$sumLines\t$sumWords\t$sumBytes\tSummary".toByteArray())
            }
        }
        return exitCode
    }

    private fun count(text: String): Triple<Int, Int, Int> = Triple(
        text.count { it == '\n' },
        "\\S+".toRegex().findAll(text).count(),
        text.toByteArray().count()
    )
}
