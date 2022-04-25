package game.roulette.wheel

import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.RepeatedTest

internal class EuropeanRouletteWheelTest {

	@BeforeEach
	fun setUp() {
	}

	private val wheel = EuropeanRouletteWheel()

	@RepeatedTest(5)
	fun `zeroCount should be always 1`() {
		assertEquals(1, wheel.zeroCount)
	}

	@RepeatedTest(5)
	fun `size should be always 37`() {
		assertEquals(37, wheel.size)
	}

	@RepeatedTest(40)
	fun `spin() should return values between 0 and 36`() {
		val value = wheel.spin()
		assertTrue(value in 0..36)
	}
}