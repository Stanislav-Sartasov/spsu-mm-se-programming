package minibash.parsing

import java.util.regex.*
import kotlin.streams.asSequence


object ParserImpl : Parser {

    override fun parse(line: String): Instruction {
        val lines = line.lines()

        return if (lines.size == 1) {
            val trimmedLine = lines.first().trim()

            if (trimmedLine.firstOrNull() == '$') {
                parseVarAssign(trimmedLine.drop(n = 1)) ?: Instruction.ThrowSyntaxError("Incorrect variable assigning")
            } else {
                parsePipe(trimmedLine) ?: Instruction.ThrowSyntaxError("Incorrect command or pipe definition")
            }
        } else {
            Instruction.ThrowSyntaxError("Too many lines")
        }
    }


    private fun parseVarAssign(trimmedLine: String) = trimmedLine
        .split('=', limit = 2)
        .takeIf { it.size == 2 }
        ?.let { (name, value) ->
            val parsedName = name
                .takeUnless { it.indexOfLast(Char::isWhitespace) == it.lastIndex }
                ?.run(::parseVarName)
            val parsedValue = value
                .takeUnless { it.indexOfFirst(Char::isWhitespace) == 0 }
                ?.run(::parseExpandableString)

            if (parsedName != null && parsedValue != null) {
                Instruction.AssignVariable(parsedName, parsedValue)
            } else {
                null
            }
        }

    private fun parsePipe(trimmedLine: String): Instruction.RunPipe? {
        val parts = buildList {
            add(0 - 1)
            var quotesCounter = 0
            for ((i, c) in trimmedLine.withIndex()) {
                when (c) {
                    '"' -> quotesCounter += 1
                    '|' -> if (quotesCounter % 2 == 0) add(i)
                }
            }
            add(trimmedLine.lastIndex + 1)
        }.asSequence()
            .zipWithNext { i, j -> i + 1 to j - 1 }
            .map { (i, j) -> i.rangeTo(j) }
            .map { it.takeUnless(IntRange::isEmpty) }

        if (parts.any { it == null }) return null

        val parsedParts = parts.filterNotNull()
            .map(trimmedLine::substring)
            .map(String::trim)
            .map(::parseCmdWithArgs)
            .toList()

        return parsedParts.filterNotNull().takeIf { it.size == parsedParts.size }?.run(Instruction::RunPipe)
    }


    private fun parseVarName(trimmed: String): String? = VAR_NAME_PATTERN.toRegex()
        .matchEntire(trimmed)
        ?.value


    private fun parseCmdWithArgs(trimmed: String): Pair<String, List<ExpandableString>>? = trimmed
        .split(regex = """\s""".toRegex(), limit = 2)
        .let { parts ->
            val name = parts.first()
            val argsString = parts.getOrElse(index = 1) { "" }
            parseCmdArgs(argsString.trim())?.let { args ->
                name.trim() to args
            }
        }

    private fun parseCmdArgs(s: String): List<ExpandableString>? {
        val pattern = """[^\s"]+|"([^"]*)"""".toPattern()
        val args = pattern.matcher(s)
            .results()
            .asSequence()
            .map(MatchResult::group)
            .map(::parseExpandableString)
            .toList()

        return args.filterNotNull().takeIf { it.size == args.size }
    }


    private fun parseExpandableString(trimmed: String): ExpandableString? {
        fun parse(patternString: String, content: String): ExpandableString? {
            if (content.isEmpty()) return ExpandableString()

            val parts = if ("($patternString)+".toRegex().matchEntire(content) != null) {
                patternString
                    .toRegex()
                    .toPattern()
                    .matcher(content)
                    .results()
                    .asSequence()
                    .map(MatchResult::group)
                    .map(::parseStringValue)
                    .toList()
            } else {
                null
            }

            return parts?.filterNotNull()?.takeIf { it.size == parts.size }?.run(::ExpandableString)
        }

        return if (trimmed.firstOrNull() == '"') {
            if (trimmed.lastOrNull() == '"' && '"' !in trimmed.substring(1, trimmed.lastIndex)) {
                parse(
                    patternString = """\$$VAR_NAME_PATTERN|[^\$]+""",
                    content = trimmed.substring(1, trimmed.lastIndex)
                )
            } else {
                null
            }
        } else {
            if ('"' !in trimmed) {
                parse(
                    patternString = """\$$VAR_NAME_PATTERN|[^\$\s]+""",
                    content = trimmed
                )
            } else {
                null
            }
        }
    }

    private fun parseStringValue(trimmed: String) = when (trimmed.lastIndexOf('$')) {
        -1 -> StringValue.Plain(value = trimmed)
        0 -> trimmed.drop(n = 1).run(::parseVarName)?.run(StringValue::Variable)
        else -> null
    }


    private const val VAR_NAME_PATTERN = """[a-zA-Z_]\w*"""
}
