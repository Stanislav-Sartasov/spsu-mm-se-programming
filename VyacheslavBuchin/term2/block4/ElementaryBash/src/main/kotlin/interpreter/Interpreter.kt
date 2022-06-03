package interpreter

import channel.Channel

interface Interpreter {
	val output: Channel<String>
	val error: Channel<String>
	val input: Channel<String>
	fun interpret(commandsWithArgs: List<List<String>>): Int
}