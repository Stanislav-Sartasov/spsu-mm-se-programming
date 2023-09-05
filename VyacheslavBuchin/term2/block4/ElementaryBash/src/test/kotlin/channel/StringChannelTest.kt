package channel

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class StringChannelTest {

	private val separator = " "
	private val stringChannel = StringChannel(separator)

	@Test
	fun `read() should return empty string if nothing was written to channel`() {
		val string = stringChannel.read()
		assertEquals("", string)
	}

	@Test
	fun `read() should return written strings separated by given separator`() {
		val string1 = "string1"
		val string2 = "string2"
		stringChannel.write(string1)
		stringChannel.write(string2)

		assertEquals("$string1$separator$string2$separator", stringChannel.read())
	}


}