package blackjackTableTests

import blackjackTable.BlackjackTable
import org.junit.jupiter.api.Test
import testPlayer.TestPlayer
import kotlin.random.Random

class BlackjackTableTests {

    @Test
    fun playBlackjackTableTest() {
        val randomListOfPlayers = List(Random.nextInt(50)) {
            TestPlayer(
                Random.nextInt(1000),
                Random.nextInt(1, 1000),
                Random.nextInt(1, 21)
            )
        }
        val testTable = BlackjackTable()

        try {
            testTable.playBlackjackAndPrintResult(randomListOfPlayers, 1)
        } catch (e: Exception) {
            assert(false) { "Error occurs when playing blackjack" }
        }

        assert(true)
    }
}