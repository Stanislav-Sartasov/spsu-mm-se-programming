package minibash.pipe.commands

import kotlinx.coroutines.flow.*
import minibash.pipe.Command
import minibash.pipe.CommandRunOut
import minibash.utils.FlowUtils.concat
import minibash.utils.FlowUtils.joinToString
import minibash.utils.FlowUtils.toCharFlow
import minibash.utils.StringUtils.createErrorMessage
import minibash.utils.StringUtils.createNoArgumentsErrorMessage
import minibash.utils.StringUtils.ln
import java.io.File

class WcCommand(override val name: String) : Command {

    override fun run(args: List<String>, input: Flow<Char>?) = if (args.isNotEmpty()) {
        args.fold(initial = CommandRunOut()) { (accOutput, accErrors), arg ->
            try {
                val text = File(arg).readText()
                CommandRunOut(
                    output = concat(accOutput, text.runWc().toCharFlow()),
                    errors = accErrors
                )
            } catch (e: Throwable) {
                CommandRunOut(
                    output = accOutput,
                    errors = concat(accErrors, createErrorMessage(name, e).toCharFlow())
                )
            }
        }
    } else {
        if (input != null) {
            CommandRunOut(output = flow { emitAll(input.joinToString().runWc().toCharFlow()) })
        } else {
            CommandRunOut(errors = createNoArgumentsErrorMessage(name).toCharFlow())
        }
    }


    private fun String.runWc() = countInfo().format()


    private fun String.countInfo(): Triple<Long, Long, Long> {
        var lines: Long = 0
        var words: Long = 0
        var bytes: Long = 0
        var isPrevByteIsWs = true

        forEach {
            if (it == '\n') lines += 1
            if (it.isWhitespace()) {
                if (!isPrevByteIsWs) words += 1
                isPrevByteIsWs = true
            } else {
                isPrevByteIsWs = false
            }
            bytes += it.toString().toByteArray().size
        }
        if (!isPrevByteIsWs) words += 1

        return Triple(lines, words, bytes)
    }

    private fun Triple<Long, Long, Long>.format(): String {
        val infoInText = toList().map(Long::toString)
        val maxLength = infoInText.maxOf(String::length)
        val padLength = generateSequence(seed = 2) { if (it <= maxLength) it * 2 else null }.last()
        val (l, w, b) = infoInText.map { it.padStart(padLength) }
        return "$l$w$b".ln()
    }
}
