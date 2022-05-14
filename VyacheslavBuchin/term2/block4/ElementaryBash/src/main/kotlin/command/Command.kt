package command

import channel.Channel

interface Command {
	val output: Channel<String>
	val error: Channel<String>
	val input: Channel<String>
	fun execute(args: Array<String>): Int
}