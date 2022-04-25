package bmp

import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class ColorTest {

	private lateinit var color: Color

	@BeforeEach
	fun setUp() {
		color = Color(42, 228, 133, 7)
	}

	@Test
	fun `rgb() should return red, green, blue components of color`() {
		val rgb = color.rgb()

		assertEquals(color.red, rgb[0])
		assertEquals(color.green, rgb[1])
		assertEquals(color.blue, rgb[2])
	}
}