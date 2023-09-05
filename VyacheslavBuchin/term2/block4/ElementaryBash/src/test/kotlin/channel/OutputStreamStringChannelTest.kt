package channel

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import java.io.ByteArrayOutputStream

internal class OutputStreamStringChannelTest {
	private val testString = "some test string"
	private val outputStream = ByteArrayOutputStream()
	private val channel = OutputStreamStringChannel(outputStream, "")

	@Test
	fun `read() should return empty stream`() {
		val result = channel.read()
		assertEquals("", result)
	}

	@Test
	fun `write() should write given string to outputStream`() {
		channel.write(testString)
		assertEquals(testString, outputStream.toString())
	}
}