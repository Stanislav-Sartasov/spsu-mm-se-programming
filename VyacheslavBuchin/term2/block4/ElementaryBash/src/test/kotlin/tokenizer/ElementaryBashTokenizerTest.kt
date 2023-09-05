package tokenizer

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class ElementaryBashTokenizerTest {

	private val tokenizer = ElementaryBashTokenizer()

	@Test
	fun `tokenizer should separate statements by non-quoted whitespaces`() {
		val statements = listOf("echo", "123", "somestring")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements, result)
	}

	@Test
	fun `separation by non-quoted whitespaces should work regardless of count of whitespaces`() {
		val statements = listOf("echo", "123", "somestring")
		val result = tokenizer.tokenize(statements.joinToString("    "))
		assertEquals(statements, result)
	}

	@Test
	fun `text in quotes should not be separated and tokens should contain no non-escaped quotes`() {
		val statements = listOf("ec\" | ho\"", "' |  some another text'", "\"some text \"")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements.map { it.replace("\"", "").replace("\'", "") }, result)
	}

	@Test
	fun `backslash before any quote should escape the quote`() {
		val statements = listOf("ec\\'ho", "123\\\"")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements.map { it.replace("\\", "") }, result)
	}

	@Test
	fun `pipe should be separate token after tokenization`() {
		val statements = listOf("123", "|", "wc")
		val result = tokenizer.tokenize(statements.joinToString(""))
		assertEquals(statements, result)
	}

	@Test
	fun `backslash before pipe should escape it`() {
		val statements = listOf("echo", "123\\|")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements.map { it.replace("\\", "") }, result)
	}

	@Test
	fun `backslash before space should escape it`() {
		val statements = listOf("echo\\ lol", "123\\|")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements.map { it.replace("\\", "") }, result)
	}

	@Test
	fun `backslash before something that is not space or pipe or any quote should be in tokens`() {
		val statements = listOf("\\echo", "123")
		val result = tokenizer.tokenize(statements.joinToString(" "))
		assertEquals(statements, result)
	}
}