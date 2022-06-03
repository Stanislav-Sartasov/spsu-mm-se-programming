package channel

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class InputStreamStringChannelTest {
	private val testString = "some test string"
	private val inputStream = testString.byteInputStream()
	private val channel = InputStreamStringChannel(inputStream)

	@Test
	fun `read() should return text from given input stream`() {
		val result = channel.read()
		assertEquals(testString, result)
	}

	@Test
	fun `write() should no effect on read()`() {
		channel.write("some bad string")
		val result = channel.read()
		assertEquals(testString, result)
	}
}