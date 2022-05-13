package blackjackPlayerTests

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import kotlin.random.Random
import testPlayer.*

class BlackjackPlayerTests {

	@Test
	fun getBetTestWithEnoughMoney() {
		val balance = Random.nextInt(0, 2147483647)
		val betAmount = Random.nextInt(0, balance)
		val testPlayer = TestPlayer(balance, betAmount)

		assertEquals(testPlayer.betAmount, testPlayer.getBet())
	}

	@Test
	fun balanceTestAfterGetBetWithEnoughMoney() {
		val balance = Random.nextInt(0, 2147483647)
		val betAmount = Random.nextInt(0, balance)
		val testPlayer = TestPlayer(balance, betAmount)

		testPlayer.getBet()
		assertEquals(balance - testPlayer.betAmount, testPlayer.balance)
	}

	@Test
	fun getBetTestWithNotEnoughMoney() {
		val balance = Random.nextInt(0, 1073741823)
		val betAmount = Random.nextInt(balance,2147483647)
		val testPlayer = TestPlayer(balance, betAmount)

		assertEquals(0, testPlayer.getBet())
	}

	@Test
	fun balanceTestAfterGetBetWithNotEnoughMoney() {
		val balance = Random.nextInt(0, 1073741823)
		val betAmount = Random.nextInt(balance, 2147483647)
		val testPlayer = TestPlayer(balance, betAmount)

		testPlayer.getBet()
		assertEquals(balance, testPlayer.balance)
	}

}