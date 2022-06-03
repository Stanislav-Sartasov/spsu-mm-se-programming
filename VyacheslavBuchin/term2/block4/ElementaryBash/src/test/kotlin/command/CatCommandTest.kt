package command

import channel.StringChannel
import exception.ElementaryBashException
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
	fun `ElementaryBashException should be thrown if file can not be opened`() {
		 assertThrows(ElementaryBashException::class.java) {
			 cat.execute(arrayOf("$resourcesPath/$nonExistingFile"))
		 }
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

	@Test
	fun `cat should write file containment if arg is a path to file`() {
		cat.execute(arrayOf("$resourcesPath/$existingFile"))
		val result = cat.output.read()
		val expectation = File("$resourcesPath/$existingFile").bufferedReader().readText()
		assertEquals(expectation, result)
	}

	@Test
	fun `ElementaryBashException should be thrown if more than 1 argument passed`() {
		assertThrows(ElementaryBashException::class.java) { cat.execute(arrayOf("arg1", "arg2")) }
	}
}