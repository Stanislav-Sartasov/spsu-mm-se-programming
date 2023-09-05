package bash

import Bash
import commands.*
import utils.BashExitException
import utils.BashInterpreterException
import utils.ILogger
import java.io.*

interface IInterpreter {
    fun interpret(ast: AST): InterpretationResult
}

data class InterpretationResult(
    val exiting: Boolean = false,
    val exitCode: Int,
    val output: InputStream,
    val error: InputStream,
)

class Interpreter(private val environment: Environment, private val logger: ILogger) : IInterpreter, Visitor {
    private var pipeInput = InputStream.nullInputStream()
    private var pipeOutput = ByteArrayOutputStream()
    private var output = ByteArrayOutputStream()
    private var error = ByteArrayOutputStream()

    override fun interpret(ast: AST): InterpretationResult {
        val exiting = try {
            execute(ast)
            false
        } catch (e: BashExitException) {
            true
        }
        val ret = InterpretationResult(
            exiting,
            environment.exitCode,
            ByteArrayInputStream(output.toByteArray()),
            ByteArrayInputStream(error.toByteArray())
        )
        output.reset()
        error.reset()
        return ret
    }

    private fun execute(ast: AST): Any {
        return ast.accept(this)
    }

    override fun visit(sequence: Sequence) {
        sequence.pipes.map {
            execute(it)
            pipeOutput.writeTo(output)
            newPipeIOStreams()
        }
    }

    override fun visit(pipe: Pipe) {
        execute(pipe.left)
        switchPipeIOStreams()
        execute(pipe.right)
    }

    override fun visit(command: SimpleCommand) {
        command.assign?.map { execute(it) }
        command.redirections?.filter { it.type == RedirectionType.In }?.map { execute(it) }
        command.cmd?.let { execute(it) }
        command.redirections?.filter { it.type == RedirectionType.Out }?.map { execute(it) }
    }

    override fun visit(command: Command) {
        val cmd = execute(command.command) as String
        val args = command.args.map { execute(it) as String }
        environment.exitCode = createCommand(cmd, args).run(
            pipeInput, pipeOutput, error, environment
        )
    }

    override fun visit(redirection: Redirection) {
        try {
            val f = execute(redirection.file) as String
            when (redirection.type) {
                RedirectionType.In -> {
                    val file = File(f)
                    if (file.isFile) {
                        pipeInput = File(f).inputStream()
                    } else {
                        logger.error("Interpreter", "$f is not a file")
                        throw BashInterpreterException()
                    }
                }

                RedirectionType.Out -> {
                    File(f).writeBytes(pipeOutput.toByteArray())
                    pipeOutput = ByteArrayOutputStream()
                }
            }
        } catch (e: IOException) {
            logger.error("Interpreter", e.toString())
        }
    }

    override fun visit(assignment: Assignment) {
        val k = assignment.key.accept(this) as String
        val v = assignment.value.accept(this) as String
        environment.set(k, v)
    }

    override fun visit(word: Word): String {
        return processWordToken(word.token)
    }

    private fun processWordToken(token: WordToken): String {
        return when (token) {
            is VarSubstitutionToken -> environment.get(token.word)
            is CommandSubstitutionToken -> runCommand(token.word)
            is CompoundWordToken -> buildString {
                token.wordlist.forEach { subToken ->
                    append(processWordToken(subToken).also { value ->
                        subToken.word = value
                    })
                }
            }

            else -> token.word
        }
    }

    private fun runCommand(word: String): String {
        val out = ByteArrayOutputStream()
        Bash(logger).main(ByteArrayInputStream(word.toByteArray()), out)
        return out.toString().dropLast(1)// trimEnd('\n')
    }

    private fun createCommand(cmd: String, args: List<String>): ACommand {      // todo: move it somewhere else?
        return when (cmd) {
            "cat" -> CatCommand(args)
            "echo" -> EchoCommand(args)
            "wc" -> WcCommand(args)
            "pwd" -> PwdCommand(args)
            "exit" -> ExitCommand(args)
            else -> ShellCommand(cmd, args)
        }
    }

    private fun switchPipeIOStreams() {
        pipeInput = ByteArrayInputStream(pipeOutput.toByteArray())
        pipeOutput = ByteArrayOutputStream()
    }

    private fun newPipeIOStreams() {
        pipeInput = InputStream.nullInputStream()
        pipeOutput = ByteArrayOutputStream()
    }
}