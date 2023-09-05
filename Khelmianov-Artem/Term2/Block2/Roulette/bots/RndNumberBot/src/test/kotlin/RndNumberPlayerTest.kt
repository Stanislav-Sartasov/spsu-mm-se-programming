import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Assertions.assertTrue
import org.junit.jupiter.api.Test
import roulette.bets.NumberBet
import roulette.bots.RndNumberPlayer
import kotlin.test.assertIs

internal class RndNumberPlayerTest {
    var player = RndNumberPlayer("", 100)

    @Test
    fun changeBalance() {
        player.changeBalance(10)
        assertEquals(player.balance, 110)
        player.changeBalance(-20)
        assertEquals(player.balance, 90)
    }

    @Test
    fun placeBet() {
        val bet = player.placeBet()
        assertIs<NumberBet>(bet)
        assertTrue(bet.bidder === player)
        assertTrue(bet.betAmount in 0..5)
    }
}