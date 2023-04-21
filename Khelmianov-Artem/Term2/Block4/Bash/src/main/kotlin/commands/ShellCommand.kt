package commands

import bash.Environment
import org.buildobjects.process.ProcBuilder
import org.buildobjects.process.StartupException
import org.buildobjects.process.TimeoutException
import java.io.InputStream
import java.io.OutputStream

class ShellCommand(cmd: String, args: List<String>) : ACommand(cmd, args) {
    override fun run(input: InputStream, output: OutputStream, error: OutputStream, env: Environment): Int {
        val process = ProcBuilder(cmd)
            .withArgs(*args.toTypedArray())
            .withVars(env.getAll())
            .withInputStream(input)
            .withOutputStream(output)
            .withErrorStream(error)
            .ignoreExitStatus()

        return try {
            val proc = process.run()
            proc.exitValue
        } catch (e: StartupException) {
            error.write("Couldn't find command '$cmd'. $e\n".toByteArray())
            return 1
        } catch (e: TimeoutException) {
            error.write("Command '$cmd' took longer then 5sec, terminating since ^C not implemented\n".toByteArray())
            return 2
        }
    }
}
