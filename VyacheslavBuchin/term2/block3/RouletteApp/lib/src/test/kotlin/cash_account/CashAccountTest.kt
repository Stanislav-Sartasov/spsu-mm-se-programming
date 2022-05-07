package cash_account

import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.math.abs

internal class CashAccountTest {

	companion object {
		@JvmStatic
		fun amounts() = listOf(
			Arguments.of(0.0),
			Arguments.of(20.0),
			Arguments.of(1e5)
		)
	}

	private lateinit var account: CashAccount
	private val epsilon = 1e-7
	private val initBalance = 100.0

	@BeforeEach
	fun setUp() {
		account = CashAccount(initBalance)
	}

	@Test
	fun `balance() should return same balance that was before creating object`() {
		assertEquals(initBalance, account.balance())
	}

	@ParameterizedTest
	@MethodSource("amounts")
	fun `addMoney() should increase balance by given value`(amount: Double) {
		account.addMoney(amount)
		assertTrue(abs(account.balance() - (initBalance + amount)) < epsilon)
	}

	@ParameterizedTest
	@MethodSource("amounts")
	fun `removeMoney() should decrease balance by given value`(amount: Double) {
		account.removeMoney(amount)
		assertTrue(abs(account.balance() - (initBalance - amount)) < epsilon)
	}

	@ParameterizedTest
	@MethodSource("amounts")
	fun `after adding and removing same amount balance should stay same`(amount: Double) {
		account.removeMoney(amount)
		account.addMoney(amount)
		assertTrue(abs(account.balance() - initBalance) < epsilon)
	}
}