package command

import exception.ElementaryBashException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class ExitCommandTest {
	private val exit = ExitCommand()

	@Test
	fun `ElementaryBashException should be thrown if given more than 1 argument`() {
		assertThrows(ElementaryBashException::class.java) { exit.execute(arrayOf("arg1", "arg2")) }
	}

	@Test
	fun `ElementaryBashException should be thrown if given more than 1 argument if given argument is not an integer`() {
		val argument = "not a number"
		assertThrows(ElementaryBashException::class.java) { exit.execute(arrayOf(argument)) }
	}
}