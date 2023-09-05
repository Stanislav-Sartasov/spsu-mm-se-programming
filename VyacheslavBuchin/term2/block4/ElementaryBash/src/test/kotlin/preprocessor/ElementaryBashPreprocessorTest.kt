package preprocessor

import exception.ElementaryBashException
import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import service.substitution.MapSubstitutionManager

internal class ElementaryBashPreprocessorTest {
	private val manager = MapSubstitutionManager()
	private val preprocessor = ElementaryBashPreprocessor(manager)
	private val variable = "var"
	private val value = "value"

	@BeforeEach
	fun setUp() {
		manager[variable] = value
	}

	@Test
	fun `substitution should be performed if $ followed by name and not in any quotes`() {
		val result = preprocessor.applySubstitutions("\$$variable")
		assertEquals(value, result)
	}

	@Test
	fun `substitution should be performed if $ followed by name is in double quotes`() {
		val result = preprocessor.applySubstitutions("\"\$$variable\"")
		assertEquals("\"$value\"", result)
	}

	@Test
	fun `substitution should not be performed if $ followed by name is in single quotes`() {
		val result = preprocessor.applySubstitutions("'\$$variable'")
		assertEquals("'\$$variable'", result)
	}

	@Test
	fun `substitution should be performed if $ followed by name in curly braces and not in any quotes`() {
		val result = preprocessor.applySubstitutions("\${$variable}")
		assertEquals(value, result)
	}

	@Test
	fun `substitution should be performed if $ followed by name in curly braces is in double quotes`() {
		val result = preprocessor.applySubstitutions("\"\${$variable}\"")
		assertEquals("\"value\"", result)
	}

	@Test
	fun `substitution should not be performed if $ followed by name in curly braces is in single quotes`() {
		val result = preprocessor.applySubstitutions("\'\${$variable}\'")
		assertEquals("'\${$variable}'", result)
	}

	@Test
	fun `substitution should not be performed if $ followed by space`() {
		val result = preprocessor.applySubstitutions("\$ $variable")
		assertEquals("\$ $variable", result)
	}

	@Test
	fun `PreprocessorException should be thrown if {} do not form the correct bracket sequence`() {
		assertThrows(ElementaryBashException::class.java) {
			preprocessor.applySubstitutions("\${$variable")
		}
	}

	@Test
	fun `PreprocessorException should be thrown if double quotes do not form the correct bracket sequence`() {
		assertThrows(ElementaryBashException::class.java) {
			preprocessor.applySubstitutions("\"\$$variable")
		}
	}

	@Test
	fun `PreprocessorException should be thrown if single quotes do not form the correct bracket sequence`() {
		assertThrows(ElementaryBashException::class.java) {
			preprocessor.applySubstitutions("\'\$$variable")
		}
	}
}