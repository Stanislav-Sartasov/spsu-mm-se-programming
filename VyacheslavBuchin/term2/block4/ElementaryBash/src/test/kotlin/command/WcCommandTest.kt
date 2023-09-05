package command

import channel.StringChannel
import exception.ElementaryBashException
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.Assertions.*
import kotlin.test.assertEquals

internal class WcCommandTest {
	private val wc = WcCommand(StringChannel(""))
	private val resourcesPath = "src/test/resources"
	private val existingFile = "some_text.txt"
	private val nonExistingFile = "non_existing_file.txt"

	@Test
	fun `wc should count lines, word, bytes`() {
		val testString = "word1 word2 w3"
		wc.input.write(testString)
		wc.execute(arrayOf())
		val result = wc.output.read()
		assertEquals("1 3 14", result.trim())
	}

	@Test
	fun `prefix, postfix and between-words spaces should have no effect to line and word count`() {
		val testString1 = "word1 word2 w3"
		val testString2 = "    word1  word2     w3  "

		wc.input.write(testString1)
		wc.execute(arrayOf())
		val result1 = wc.output.read().split(" ")
		wc.input.write(testString2)
		wc.execute(arrayOf())
		val result2 = wc.output.read().split(" ")

		assertEquals(result1[0], result2[0])
		assertEquals(result1[1], result2[1])
	}

	@Test
	fun `empty line should be counted as line`() {
		wc.input.write("")
		wc.execute(arrayOf())
		assertEquals("0 0 0", wc.output.read().trim())
	}

	@Test
	fun `ElementaryBashException should be thrown if cannot open file by given path`() {
		assertThrows(ElementaryBashException::class.java) { wc.execute(arrayOf("$resourcesPath/$nonExistingFile")) }
	}

	@Test
	fun `command should ignore input if given args is not empty`() {
		wc.input.write("some text")
		wc.execute(arrayOf("$resourcesPath/$existingFile"))
		val result = wc.output.read()

		assertEquals("3 8 43", result.trim())
	}

	@Test
	fun `ElementaryBashException should be thrown if more than 1 argument passed`() {
		assertThrows(ElementaryBashException::class.java) { wc.execute(arrayOf("arg1", "arg2")) }
	}
}