package command

import channel.Channel

interface Command {
	var output: Channel<String>
	var error: Channel<String>
	var input: Channel<String>
	fun execute(args: Array<String>): Int
}