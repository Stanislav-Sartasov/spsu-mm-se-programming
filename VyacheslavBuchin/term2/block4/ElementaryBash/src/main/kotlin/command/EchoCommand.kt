package command
import channel.Channel
import channel.StringChannel

class EchoCommand : Command {

	override var input = StringChannel.nullChannel()
	override var error: Channel<String> = StringChannel()
	override var output: Channel<String> = StringChannel("")

	override fun execute(args: Array<String>): Int {
		output.write(args.joinToString(" "))
		return 0
	}
}