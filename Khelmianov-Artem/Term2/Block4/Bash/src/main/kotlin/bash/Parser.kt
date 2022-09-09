package bash

import utils.BashParserException
import utils.ILogger
import utils.getClassName
import utils.isNullCommand

interface IParser {
    fun parse(): AST
}

class Parser(private var tokens: ArrayList<Token>, private val logger: ILogger) : IParser {
    private var finished = tokens.isEmpty()
    private var current = 0
    private var currentToken = if (!finished) tokens[0] else EOFToken(0, 0)

    override fun parse(): AST {
        return sequence()
    }

    private fun sequence(): AST {
        val pipes = buildList {
            add(pipe())
            while (currentToken is SemicolonToken) {
                advance()
                add(pipe())
            }
        }
        consume<EOFToken>("Expected EOF token, got ${getClassName(currentToken)} ${currentToken.position()}")
        return Sequence(pipes)
    }

    private fun pipe(): AST {
        var commands = simpleCommand()
        while (currentToken is PipeToken) {
            advance()
            commands = Pipe(commands, pipe())
        }
        if (commands is Pipe) {
            if ((commands.left as SimpleCommand).isNullCommand()) {
                logger.error("Parser", "Piping from nothing at line ${currentToken.position()}")
                throw BashParserException()
            }
            if (commands.right is SimpleCommand && (commands.right as SimpleCommand).isNullCommand()) {
                logger.error("Parser", "Piping to nothing at line ${currentToken.position()}")
                throw BashParserException()
            }
        }
        return commands
    }

    private fun simpleCommand(): AST {      // TODO: this
        val assignments = buildList {
            while (currentToken is AssignmentToken) {
                add(assignment())
                advance()
            }
        }.ifEmpty { null }
        val cmd = if (currentToken is WordToken) {
            command()
        } else {
            null
        }
        val redirect = if (cmd != null) {
            buildList {
                while (currentToken is RedirectionToken) {
                    add(redirection())
                }
            }.ifEmpty { null }
        } else {
            null
        }
        return SimpleCommand(assignments, cmd, redirect)
    }

    private fun redirection(): Redirection {
        val redirection = consume<RedirectionToken>() as RedirectionToken
        val word = word()
        return Redirection(redirection.direction, word)
    }

    private fun command(): Command {
        val cmd = word()
        val args = buildList {
            while (currentToken is WordToken) {
                add(word())
            }
        }
        return Command(cmd, args)
    }

    private fun assignment(): Assignment {
        val token: AssignmentToken = currentToken as AssignmentToken
        return Assignment(Word(token.key), Word(token.value))
    }

    private fun word(): Word {
        val token = consume<WordToken>(
            "Expected WordToken, got ${getClassName(currentToken)} at ${currentToken.position()}"
        ) as WordToken
        return Word(token)
    }

    private fun advance() = try {
        currentToken = tokens[++current]
    } catch (e: IndexOutOfBoundsException) {
        finished = true
    }

    private inline fun <reified T> consume(msg: String = ""): Token {
        if (currentToken is T) {
            val ret = currentToken
            advance()
            return ret
        } else {
            logger.error("Parser", msg)
            throw BashParserException()
        }
    }
}
