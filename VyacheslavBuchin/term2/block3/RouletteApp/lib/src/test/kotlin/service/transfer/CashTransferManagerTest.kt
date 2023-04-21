package service.transfer

import cash_account.CashAccount

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class CashTransferManagerTest {

	private val initBalance = 100.0
	private val smallAmount = 50.0
	private val bigAmount = 150.0
	private val accountFrom = CashAccount(initBalance)
	private val accountTo = CashAccount(initBalance)
	private val transferManager = CashTransferManager()

	@Test
	fun `transfer() should transfer given amount from accountFrom to accountTo if accountTo balance is not less than given amount`() {
		transferManager.transfer(accountFrom, accountTo, smallAmount)

		assertEquals(initBalance - smallAmount, accountFrom.balance())
		assertEquals(initBalance + smallAmount, accountTo.balance())
	}

	@Test
	fun `transfer() should not change balances of accountFrom and accountTo if accountTo balance is less than given amount`() {
		transferManager.transfer(accountFrom, accountTo, bigAmount)

		assertEquals(initBalance, accountFrom.balance())
		assertEquals(initBalance, accountTo.balance())
	}

	@Test
	fun `transfer() should return true if accountFrom has more or equal balance than given amount`() {
		val result = transferManager.transfer(accountFrom, accountTo, smallAmount)
		assertTrue(result)
	}

	@Test
	fun `transfer() should return false if accountFrom has less balance than given amount`() {
		val result = transferManager.transfer(accountFrom, accountTo, bigAmount)
		assertFalse(result)
	}

}