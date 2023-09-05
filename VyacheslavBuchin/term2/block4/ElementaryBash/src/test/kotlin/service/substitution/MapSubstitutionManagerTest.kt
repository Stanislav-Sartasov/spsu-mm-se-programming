package service.substitution

import exception.ElementaryBashException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class MapSubstitutionManagerTest {
	private val substitutionManager = MapSubstitutionManager()

	@Test
	fun `get() should return empty string if substitution was not registered`() {
		assertEquals("", substitutionManager["some_unknown_name"])
	}

	@Test
	fun `get(name) should return the same value after set(name, value)`() {
		substitutionManager["name"] = "value"
		assertEquals("value", substitutionManager["name"])
	}

	@Test
	fun `get(name) should throw SubstitutionException if name contains any whitespaces`() {
		assertThrows(ElementaryBashException::class.java) {
			substitutionManager["some name with whitespaces"]
		}
	}

	@Test
	fun `get(name) should throw SubstitutionException if name is empty`() {
		assertThrows(ElementaryBashException::class.java) {
			substitutionManager[""]
		}
	}
}