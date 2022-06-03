package command

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class PwdCommandTest {

	private val pwd = PwdCommand()

	@Test
	fun `output should not be empty after execute()`() {
		pwd.execute(arrayOf())

		val result = pwd.output.read()
		assertNotEquals("", result)
	}

	@Test
	fun `writing anything to input should not change behavior of pwd command`() {
		val testPwd = PwdCommand()
		testPwd.input.write("42")
		testPwd.input.write("228")

		pwd.execute(arrayOf())
		testPwd.execute(arrayOf())
		val referenceResult = pwd.output.read()
		val testResult = testPwd.output.read()

		assertEquals(referenceResult, testResult)
	}

	@Test
	fun `error should be empty after execute()`() {
		pwd.execute(arrayOf())
		val result = pwd.error.read()
		assertEquals("", result.trim())
	}

	@Test
	fun `return code should be 0 after execute()`() {
		val code = pwd.execute(arrayOf())
		assertEquals(0, code)
	}

}