package bash

import utils.BashLexerException
import utils.ILogger

interface ILexer {
    fun lex(): ArrayList<Token>
}

class Lexer(private val source: String, private val logger: ILogger) : ILexer {
    private var tokens = arrayListOf<Token>()
    private var current = 0
    private var line = 1
    private var pos = 1
    private var finished = source.isEmpty()
    private var currentChar = if (!finished) source[0] else '\u0000'
    private val spaces = arrayOf(' ', '\t', '\n')

    //    private val reservedWords = mapOf<String, Token>()

    override fun lex(): ArrayList<Token> {            // todo: state machine
        while (!finished) {
            when (currentChar) {
                ';' -> {
                    tokens.add(SemicolonToken(line, pos))
                    advance()
                }

                '|' -> {
                    tokens.add(PipeToken(line, pos))
                    advance()
                }

                '#' -> comment()

                '>', '<' -> redirection()

                '\n' -> {
                    if (tokens.isNotEmpty() && tokens[tokens.lastIndex] !is SemicolonToken) {
                        tokens.add(SemicolonToken(line, pos))
                    }
                    advance()
                }

                in spaces -> advance()

                else -> tokens.add(word())
            }
        }
        tokens.add(EOFToken(line, pos))
        return tokens
    }

    private fun redirection() {
        val direction = when (currentChar.toString()) {
            "<" -> RedirectionType.In
            ">" -> RedirectionType.Out
            else -> {
                logger.error("Lexer", "")
                throw BashLexerException()
            }
        }
        advance()
        tokens.add(RedirectionToken(direction, line, pos))
    }

    private fun word(allowAssignments: Boolean = true): Token {
        var start = pos
        val result = CompoundWordToken(line, pos)
        val buffer = StringBuilder()

        fun addNotEmptyToken() {
            if (buffer.isNotEmpty()) {
                result.addToken(WordToken(buffer.toString(), line, start))
                buffer.clear()
                start = pos
            }
        }

        while (!finished) {
            when (currentChar) {
                in spaces -> if (unescaped()) {
                    break
                } else {
                    buffer.append(currentChar)
                    advance()
                }

                '\\' -> {
                    advance()
                }

                '|', ';', '#' -> {
                    addNotEmptyToken()
                    break
                }

                '$' -> if (unescaped()) {
                    addNotEmptyToken()
                    result.addToken(substitution())
                } else {
                    buffer.append(currentChar)
                    advance()
                }

                '=' -> if (allowAssignments && unescaped() && buffer.isNotEmpty() && isValidId(buffer.toString())) {
                    val key = WordToken(buffer.toString(), line, start)
                    buffer.clear()
                    advance()
                    val value = word(false)
                    return AssignmentToken(key, value as WordToken, line, start)
                } else {
                    buffer.append(currentChar)
                    advance()
                }

                '"' -> if (unescaped()) {
                    addNotEmptyToken()
                    result.addToken(doubleQuote())
                }

                '\'' -> if (unescaped()) {
                    addNotEmptyToken()
                    result.addToken(singleQuote())
                }

                else -> {
                    buffer.append(currentChar)
                    advance()
                }
            }
        }
        addNotEmptyToken()
        return if (result.wordlist.size == 1) {
            result.wordlist[0]
        } else {
            result
        }
    }

    private fun doubleQuote(): WordToken {
        val result = CompoundWordToken(line, pos)
        val buffer = StringBuilder()
        advance()
        var start = pos

        fun addNotEmptyToken() {
            if (buffer.isNotEmpty()) {
                result.addToken(WordToken(buffer.toString(), line, start))
                buffer.clear()
                start = pos
            }
        }
        while (!finished) {
            when (currentChar) {
                '"' -> if (unescaped()) break else {
                    buffer.append(currentChar)
                    advance()
                }

                '$' -> if (unescaped()) {
                    addNotEmptyToken()
                    result.addToken(substitution())
                    start = pos
                } else {
                    buffer.append(currentChar)
                    advance()
                }

                else -> {
                    buffer.append(currentChar)
                    advance()
                }
            }
        }
        consume('"', "Expected closing double quote near [$line:$pos]")
        addNotEmptyToken()
        return if (result.wordlist.size == 1) {
            result.wordlist[0]
        } else {
            result
        }
    }

    private fun singleQuote(): WordToken {
        val start = pos
        return WordToken(
            buildString {
                advance()
                while (!finished) {
                    if (currentChar == '\'' && unescaped()) break
                    append(currentChar)
                    advance()
                }
                consume('\'', "Expected closing single quote near [$line:$pos]")
            }, line, start
        )
    }

    private fun substitution(): WordToken {     // TODO: this
        var isCommand = false
        val start = pos
        val value = buildString {
            advance()
            if (!finished && currentChar == '(') {
                isCommand = true
                advance()
                while (!finished && currentChar != ')') {
                    append(currentChar)
                    advance()
                }
                consume(')')
            } else if (!finished && isIdStart()) {
                append(currentChar)
                advance()
                while (!finished && isIdSuffix()) {
                    append(currentChar)
                    advance()
                }
            }
        }
        return if (isCommand)
            CommandSubstitutionToken(value, line, start)
        else
            VarSubstitutionToken(value, line, start)
    }

    private fun comment() {
        while (currentChar != '\n' && !finished) {
            advance()
        }
    }

    private fun isIdStart(c: Char = currentChar): Boolean = c.isLetter() || c == '_'

    private fun isIdSuffix(c: Char = currentChar): Boolean = c.isLetterOrDigit() || c == '_'

    private fun isValidId(word: String): Boolean =
        isIdStart(word[0]) && word.fold(true) { acc, c -> acc && isIdSuffix(c) }

    private fun unescaped() = if (current == 0) {
        true
    } else {
        source[current - 1] != '\\'
    }

    private fun advance() = try {
        if (source[current] == '\n') {
            pos = 1
            line++
        } else {
            pos++
        }
        currentChar = source[++current]
    } catch (e: IndexOutOfBoundsException) {
        finished = true
    }

    private fun consume(c: Char, msg: String = "$line:$pos") {
        if (!finished && currentChar == c) {
            advance()
        } else {
            logger.error("Lexer", msg)
            throw BashLexerException()
        }
    }
}
