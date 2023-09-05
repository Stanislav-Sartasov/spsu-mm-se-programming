package interpreter

import channel.StringChannel
import command.CatCommand
import command.EchoCommand
import exception.ElementaryBashException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import util.ElementaryBashCommands

internal class ElementaryBashInterpreterTest {
	private val input = StringChannel()
	private val output = StringChannel("")
	private val error = StringChannel()
	private val interpreter = ElementaryBashInterpreter(
		ElementaryBashCommands.getBuiltInCommandsManager(),
		input, output, error
	)

	@Test
	fun `interpreter should execute single command the same way that command executes itself`() {
		val echo = EchoCommand()

		echo.execute(arrayOf("123", "arg2"))
		interpreter.interpret(
			listOf(listOf("echo", "123", "arg2"))
		)

		assertEquals(
			echo.output.read(),
			interpreter.output.read()
		)
	}

	@Test
	fun `pipe should transfer output from one command to another command input`() {
		val echo = EchoCommand()
		val cat = CatCommand(echo.output)

		echo.execute(arrayOf("123", "arg2"))
		cat.execute(arrayOf())
		interpreter.interpret(
			listOf(
				listOf("echo", "123", "arg2"),
				listOf("cat")
			)
		)

		assertEquals(
			cat.output.read(),
			interpreter.output.read()
		)
	}

	@Test
	fun `ElementaryBashException should be thrown if ElementaryBashException was caught`() {
		assertThrows(ElementaryBashException::class.java) {
			interpreter.interpret(
				listOf(
					listOf("some non-existing system command HFDHODnhdhfiaHfIOH", "123", "arg2"),
				)
			)
		}
	}
}