package minibash.pipe.commands

import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.SequenceUtils.concat
import minibash.utils.StringUtils.createErrorMessage
import minibash.utils.StringUtils.createNoArgumentsErrorMessage
import minibash.utils.StringUtils.ln
import java.io.File

class WcCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Sequence<Char>?) = if (args.isNotEmpty()) {
        args.fold(initial = CommandRunOut()) { (accOutput, accErrors), arg ->
            try {
                val text = File(arg).readText()
                CommandRunOut(
                    output = concat(accOutput, text.runWc().asSequence()),
                    errors = accErrors
                )
            } catch (e: Throwable) {
                CommandRunOut(
                    output = accOutput,
                    errors = concat(accErrors, createErrorMessage(name, e).asSequence())
                )
            }
        }
    } else {
        if (input != null) {
            CommandRunOut(output = input.joinToString(separator = "").runWc().asSequence())
        } else {
            CommandRunOut(errors = createNoArgumentsErrorMessage(name).asSequence())
        }
    }


    private fun String.runWc() = countInfo().format()


    private fun String.countInfo() = Triple(
        count { it == '\n' },
        """\S+""".toRegex().findAll(input = this).count(),
        asSequence().sumOf { it.toString().toByteArray().count() }
    )

    private fun Triple<Int, Int, Int>.format(): String {
        val infoInText = toList().map(Int::toString)
        val maxLength = infoInText.maxOf(String::length)
        val padLength = generateSequence(seed = 2) { if (it <= maxLength) it * 2 else null }.last()
        val (l, w, b) = infoInText.map { it.padStart(padLength) }
        return "$l$w$b".ln()
    }
}
