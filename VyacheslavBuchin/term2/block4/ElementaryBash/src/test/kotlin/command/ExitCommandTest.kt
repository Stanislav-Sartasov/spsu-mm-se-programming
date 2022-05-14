package command

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class ExitCommandTest {
	private val exit = ExitCommand()

	@Test
	fun `error should contain message if given more than 1 argument`() {
		exit.execute(arrayOf("arg1", "arg2"))
		val error = exit.error.read()
		assertEquals("too many arguments", error.trim())
	}

	@Test
	fun `error should contain message if given argument is not an integer`() {
		val argument = "not a number"
		exit.execute(arrayOf(argument))
		val error = exit.error.read()
		assertEquals("$argument: numeric argument required", error.trim())
	}
}