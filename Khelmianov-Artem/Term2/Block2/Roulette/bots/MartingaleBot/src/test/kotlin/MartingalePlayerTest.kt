import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Assertions.assertTrue
import org.junit.jupiter.api.Test
import roulette.SpinResult
import roulette.bets.ParityBet
import roulette.bots.MartingalePlayer
import kotlin.test.assertIs

internal class MartingalePlayerTest {
    val player = MartingalePlayer("", 100)

    @Test
    fun placeBet() {
        var bet = player.placeBet()
        assertIs<ParityBet>(bet)
        assertTrue(bet.bidder === player)
        assertEquals(bet.betAmount, 2)

        bet.calculateWinnings(SpinResult(3))
        bet = player.placeBet()
        assertEquals(bet.betAmount, 4)

        bet.calculateWinnings(SpinResult(4))
        bet = player.placeBet()
        assertEquals(bet.betAmount, 2)
    }
}