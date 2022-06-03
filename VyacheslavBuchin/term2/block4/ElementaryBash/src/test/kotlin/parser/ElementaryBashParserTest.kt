package parser

import exception.ElementaryBashException
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class ElementaryBashParserTest {
	private val parser = ElementaryBashParser()

	@Test
	fun `parse should split tokens by pipe`() {
		val tokens = listOf("echo", "123", "|", "wc")
		val result = parser.parse(tokens)
		assertEquals(listOf(listOf("echo", "123"), listOf("wc")), result)
	}

	@Test
	fun `parse should throw ParseException if last token is pipe`() {
		val tokens = listOf("echo", "123", "|")
		assertThrows(ElementaryBashException::class.java) {
			parser.parse(tokens)
		}
	}
}