package command

import exception.ElementaryBashException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import service.substitution.MapSubstitutionManager

internal class AssignCommandTest {
	private val substitutionManager = MapSubstitutionManager()
	private val command = AssignCommand(substitutionManager)

	@Test
	fun `command should assign string after first = character to string before first = character in given substitution manager`() {
		val name = "name"
		val value = "some=value"
		command.execute(arrayOf("$name=$value"))
		assertEquals(value, substitutionManager[name])
	}

	@Test
	fun `ElementaryBashException should be thrown if more than 1 argument passed`() {
		assertThrows(ElementaryBashException::class.java) {
			command.execute(arrayOf("arg1", "arg2"))
		}
	}
}