package service

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class MapSubstitutionManagerTest {
	private val substitutionManager = MapSubstitutionManager()

	@Test
	fun `get() should return empty string if substitution was not registered`() {
		assertEquals("", substitutionManager["some unknown name"])
	}

	@Test
	fun `get(name) should return the same value after set(name, value)`() {
		substitutionManager["name"] = "value"
		assertEquals("value", substitutionManager["name"])
	}
}