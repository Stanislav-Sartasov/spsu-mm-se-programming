package command

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class EchoCommandTest {

	private val echo = EchoCommand()

	@Test
	fun `output of echo should contain args separated by space if execute was called`() {
		echo.execute(arrayOf("42", "228"))

		val result = echo.output.read()
		assertEquals("42 228", result.trim())
	}

	@Test
	fun `output should contain argument itself if only one argument was passed to execute`() {
		echo.execute(arrayOf("elementary bash is the best!!!1!1!"))

		val result = echo.output.read()
		assertEquals("elementary bash is the best!!!1!1!", result.trim())
	}

	@Test
	fun `output of echo should be empty if execute was called with empty args`() {
		echo.execute(arrayOf())
		val result = echo.output.read()
		assertEquals("", result.trim())
	}

	@Test
	fun `execute should exit with 0 return code`() {
		val code = echo.execute(arrayOf())
		assertEquals(0, code)
	}

	@Test
	fun `input should have no effect on execute method work`() {
		echo.input.write("228")
		echo.input.write("337")
		echo.execute(arrayOf("elementary bash is the best!!!1!1!"))

		val result = echo.output.read()
		assertEquals("elementary bash is the best!!!1!1!", result.trim())
	}

	@Test
	fun `error should be empty`() {
		echo.execute(arrayOf("elementary bash is the best!!!1!1!"))

		val result = echo.error.read()
		assertEquals("", result.trim())
	}
}