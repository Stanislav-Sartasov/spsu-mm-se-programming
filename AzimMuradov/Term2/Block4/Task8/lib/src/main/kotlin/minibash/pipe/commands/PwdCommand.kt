package minibash.pipe.commands

import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.StringUtils.createTooManyArgumentsErrorMessage
import minibash.utils.StringUtils.ln
import java.io.File
import java.nio.file.Path

class PwdCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Sequence<Char>?) = if (args.isEmpty()) {
        val wd = Path.of("").toRealPath().toString()
        val ls = File(wd).listFiles()
            ?.sortedWith(Comparator.comparing<File, Boolean> { !it.isDirectory }.thenComparing<String> { it.name })
            ?.joinToString(separator = "".ln()) {
                if (it.isDirectory) "${it.name}${File.separator}" else it.name
            } ?: ""
        CommandRunOut(output = "${wd.ln().ln()}${ls.ln()}".asSequence())
    } else {
        CommandRunOut(errors = createTooManyArgumentsErrorMessage(name).asSequence())
    }
}
