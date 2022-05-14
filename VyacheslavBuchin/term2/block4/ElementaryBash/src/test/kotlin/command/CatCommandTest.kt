package command

import channel.StringChannel
import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import java.io.File

internal class CatCommandTest {
	private val cat = CatCommand(StringChannel(""))
	private val resourcesPath = "src/test/resources"
	private val existingFile = "some_text.txt"
	private val nonExistingFile = "non_existing_file.txt"
	private lateinit var testString: String

	@BeforeEach
	fun setUp() {
		File("$resourcesPath/$existingFile").bufferedReader().use {
			testString = it.readText()
		}
	}

	@Test
	fun `exit code should not be 0 if some error occurred`() {
		val code = cat.execute(arrayOf("$resourcesPath/$nonExistingFile"))
		kotlin.test.assertNotEquals(0, code)
	}

	@Test
	fun `command should ignore input if given args is not empty`() {
		cat.input.write("some string")
		cat.execute(arrayOf("$resourcesPath/$existingFile"))
		val result = cat.output.read()

		kotlin.test.assertEquals(testString, result)
	}

	@Test
	fun `command should write to output everything that read from input if no args passed`() {
		cat.input.write(testString)
		cat.execute(arrayOf())
		assertEquals(testString, cat.output.read())
	}
}