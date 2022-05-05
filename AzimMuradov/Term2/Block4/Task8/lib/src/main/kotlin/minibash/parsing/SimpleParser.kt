package minibash.parsing

import minibash.utils.Either

/**
 * Simplified minibash parser.
 *
 * Grammar (described informally):
 *
 * INSTRUCTION    : VAR_ASSIGN|PIPE|None
 *
 * VAR_ASSIGN     : \$VAR_NAME=VAR_VALUE
 * PIPE           : CMD_WITH_ARGS(\s*\|\s*CMD_WITH_ARGS)*
 * None           : \s*
 *
 * VAR_VALUE      : EXP_WORD
 *
 * CMD_WITH_ARGS  : CMD\s*(\s*CMD_ARG)*
 * CMD            : WORD
 * CMD_ARG        : EXP_WORD
 *
 * EXP_STRING     : VAR|QUOTED|WORD
 * VAR            : \$VAR_NAME
 * QUOTED         : "$QUOTED_ELEMENT*"
 * QUOTED_ELEMENT : VAR|[^$"]+
 * WORD           : [^\s$|"]+
 *
 * VAR_NAME       : [a-zA-Z_]\w*
 */
object SimpleParser : Parser {

    override fun parse(line: String): Instruction {
        val lines = line.trim().lines()

        return if (lines.size == 1) {
            val trimmedLine = lines.first().trim()

            when (trimmedLine.firstOrNull()) {
                '$' -> if (VAR_ASSIGN_REGEX.matches(trimmedLine)) {
                    parseVariableAssignment(trimmedLine)
                } else {
                    Instruction.SyntaxError(INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE)
                }
                null -> {
                    Instruction.None
                }
                else -> if (PIPE_REGEX.matches(trimmedLine)) {
                    parsePipe(trimmedLine)
                } else {
                    Instruction.SyntaxError(INCORRECT_COMMAND_OR_PIPE_SYNTAX_ERROR_MESSAGE)
                }
            }
        } else {
            Instruction.SyntaxError(TOO_MANY_LINES_SYNTAX_ERROR_MESSAGE)
        }
    }


    const val INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE = "Incorrect variable assigning"

    const val INCORRECT_COMMAND_OR_PIPE_SYNTAX_ERROR_MESSAGE = "Incorrect command or pipe definition"

    const val TOO_MANY_LINES_SYNTAX_ERROR_MESSAGE = "Too many lines"


    private fun parseVariableAssignment(trimmedLine: String) = trimmedLine
        .drop(n = 1)
        .split('=', limit = 2)
        .let { (name, value) ->
            Instruction.VariableAssignment(name = name, value = parseExpandableString(value))
        }

    private fun parsePipe(trimmedLine: String) = buildList {
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
        .map(trimmedLine::substring)
        .map(String::trim)
        .map(::parseCommandWithArguments)
        .toList()
        .run(Instruction::Pipe)


    private fun parseCommandWithArguments(trimmed: String) = trimmed
        .split(regex = """\s""".toRegex(), limit = 2)
        .let { parts ->
            val name = parts.first().trim()
            val args = EXP_STRING_REGEX
                .findAll((parts.getOrNull(index = 1) ?: "").trim())
                .map(MatchResult::value)
                .map(::parseExpandableString)
                .toList()

            name to args
        }

    private fun parseExpandableString(trimmed: String) = when (trimmed.first()) {
        '"' -> {
            val content = trimmed.substring(1, trimmed.lastIndex)

            val parsedContent = QUOTED_ELEMENT_REGEX
                .findAll(content)
                .map(MatchResult::value)
                .map {
                    if (it.first() == '$') {
                        Either.Left(ExpandableString.Variable(name = it.drop(n = 1)))
                    } else {
                        Either.Right(it)
                    }
                }
                .toList()

            ExpandableString.Quoted(parsedContent)
        }
        '$' -> ExpandableString.Variable(name = trimmed.drop(n = 1))
        else -> ExpandableString.Word(trimmed)
    }


    private const val VAR_NAME = """[a-zA-Z_]\w*"""

    private const val VAR = """\$$VAR_NAME"""
    private const val WORD = """[^\s$|"]+"""

    private const val QUOTED_ELEMENT = """($VAR|[^$"]+)"""

    private const val QUOTED = """"$QUOTED_ELEMENT*""""

    private const val EXP_STRING = """($VAR|$QUOTED|$WORD)"""

    private const val CMD_WITH_ARGS = """$WORD\s*(\s*$EXP_STRING)*"""

    private const val VAR_ASSIGN = """\$$VAR_NAME=$EXP_STRING"""
    private const val PIPE = """$CMD_WITH_ARGS(\s*\|\s*$CMD_WITH_ARGS)*"""


    // RegExs

    private val VAR_ASSIGN_REGEX = VAR_ASSIGN.toRegex()

    private val PIPE_REGEX = PIPE.toRegex()

    private val EXP_STRING_REGEX = EXP_STRING.toRegex()

    private val QUOTED_ELEMENT_REGEX = QUOTED_ELEMENT.toRegex()
}
