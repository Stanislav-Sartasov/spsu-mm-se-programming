package bet

import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.RepeatedTest
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource

internal class StraightBetTest {

	companion object {
		@JvmStatic
		fun values() = (0..36).toList().map { Arguments.of(it) }
	}

	private val wonValue = 15
	private var straightBet = StraightBet(wonValue)

	@BeforeEach
	fun setUp() {
	}

	@ParameterizedTest
	@MethodSource("values")
	fun `isWon() should return true only if value is equal to bet value`(value: Int) {
		assertEquals(value == wonValue, straightBet.isWon(value))
	}

	@RepeatedTest(10)
	fun `wonValuesCount() should always return 1`() {
		assertEquals(1, straightBet.wonValuesCount())
	}

}