package utils

import bash.*

fun getClassName(obj: Any): String = obj.javaClass.simpleName

fun SimpleCommand.isNullCommand(): Boolean {
    return assign == null && cmd == null && redirections == null
}

object LexerPrettyPrinter {
    operator fun invoke(tokens: ArrayList<Token>): String = buildString {
        if (tokens.isNotEmpty()) {
            var line = tokens.first().line
            tokens.forEach { token ->
                if (token.line != line) {
                    append('\n')
                    line = token.line
                }
                append("$token ")
            }
            if (this.isNotEmpty()) {
                append('\n')
            }
        }
    }
}

class ASTPrettyPrinter : Visitor {
    private var indent = 0

    fun run(ast: AST): String {
        return ast.accept(this) as String
    }

    override fun visit(sequence: Sequence): String {
        return toString("Sequence:", *sequence.pipes.toTypedArray())
    }

    override fun visit(pipe: Pipe): String {
        return toString("Pipe:", pipe.left, pipe.right)
    }

    override fun visit(command: SimpleCommand): String {
        return buildString {
            append(toString("Simple command: "))
            if (command.assign != null) {
                append(toString("", *command.assign.toTypedArray()))
            }
            if (command.cmd != null) {
                append(toString("", command.cmd))
            }
            if (command.redirections != null) {
                append(toString("", *command.redirections.toTypedArray()))
            }
        }
    }

    override fun visit(assignment: Assignment): String {
        return toString("Assignment:", assignment.key, assignment.value)
    }

    override fun visit(command: Command): String {
        return toString("Command:", command.command, *command.args.toTypedArray())
    }

    override fun visit(redirection: Redirection): Any {
        return toString("Redirection: ${redirection.type}", redirection.file)
    }

    override fun visit(word: Word): String {
        return word.token.let {
            if (it is CompoundWordToken) {
                it.wordlist.joinToString(separator = "") { it.word }
            } else {
                it.word
            }
        }
    }

    private fun toString(name: String, vararg exprs: AST): String {
        val builder = StringBuilder()
        builder.append(name)

        indent++
        for (expr in exprs) {
            builder.append('\n').append("\t".repeat(indent))

            builder.append(expr.accept(this))
//            when (expr) {
//                is Assignment -> builder.append("Assign: ${expr.key} = ${expr.value}")
//                is Command -> {
//                    builder.append("Command: ${expr.command.accept(this)}, Args = [")
//                    expr.args.map { builder.append(" ${it.accept(this)}") }
//                    builder.append(" ]")
//                }
//                else -> builder.append(expr.accept(this))
//            }
        }
        indent--
        return builder.toString()
    }
}
