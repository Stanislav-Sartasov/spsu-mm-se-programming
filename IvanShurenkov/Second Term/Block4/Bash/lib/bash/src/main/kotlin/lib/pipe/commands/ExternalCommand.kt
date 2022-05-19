package lib.pipe.commands

import lib.pipe.Output
import java.io.File
import java.io.IOException
import java.nio.file.Paths
import java.util.concurrent.TimeUnit

class ExternalCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        val path = Paths.get("").toRealPath().toString()
        val processBuilder = ProcessBuilder(name, *args)
            .redirectInput(File.createTempFile("bashTmp$name", null).apply { if (input != null) writeText(input) })

        try {
            val proc = processBuilder.start()
            proc.waitFor(60, TimeUnit.MINUTES)
            return Output(proc.inputStream.bufferedReader().readText(), proc.errorStream.bufferedReader().readText())
        } catch (e: IOException) {
            return Output(null, e.message + System.lineSeparator())
        }
    }
}