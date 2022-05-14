package command
import channel.StringChannel

class EchoCommand : Command {

	override val input = StringChannel.nullChannel()
	override val error = StringChannel()
	override val output = StringChannel()

	override fun execute(args: Array<String>): Int {
		output.write(args.joinToString(" "))
		return 0
	}
}