package command

import channel.StringChannel
import kotlin.io.path.Path
import kotlin.io.path.listDirectoryEntries
import kotlin.io.path.name

class PwdCommand : Command {

	override val output = StringChannel()
	override val error = StringChannel()
	override val input = StringChannel.nullChannel()

	override fun execute(args: Array<String>): Int {
		val currentDirectory = Path("")
		output.write(currentDirectory.toAbsolutePath().toString())
		val directories = currentDirectory.listDirectoryEntries()
		directories.forEach {
			output.write(it.name)
		}
		return 0
	}
}