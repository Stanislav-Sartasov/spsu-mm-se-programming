package lib.pipe.commands

import lib.pipe.Output
import java.io.File
import java.nio.file.Paths

class PwdCommand(val name: String) : ICommand {
    override fun run(args: Array<String>, input: String?): Output {
        val path = Paths.get("").toRealPath().toString()

        var ls = ""
        val folders = File(path).listFiles { file -> file.isDirectory }
        folders?.forEach { folder -> ls += "${folder.name}/${System.lineSeparator()}" }
        val files = File(path).listFiles { file -> file.isFile }
        files?.forEach { file -> ls += "${file.name}${System.lineSeparator()}" }
        return Output("$path${System.lineSeparator()}$ls")
    }
}