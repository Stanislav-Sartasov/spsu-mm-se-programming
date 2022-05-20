package service.command

import command.CatCommand
import command.OSCommand

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import service.substitution.MapSubstitutionManager

internal class MapCommandManagerTest {
	private val manager = MapCommandManager(MapSubstitutionManager())

	@Test
	fun `get() should return OSCommand if command was not registered`() {
		val command = manager["true"]
		assertTrue(command is OSCommand)
	}

	@Test
	fun `get(name) should return the same value after set(name, value)`() {
		manager["cat"] = { CatCommand() }
		assertTrue(manager["cat"] is CatCommand)
	}
}