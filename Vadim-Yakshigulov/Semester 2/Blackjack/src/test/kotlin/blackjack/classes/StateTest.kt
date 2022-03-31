package blackjack.classes

import blackjack.actions.DistributeCardsInitiallyAction
import blackjack.actions.PlayersMoveAction
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import utils.DrawGame
import utils.LostGame
import utils.SimpleIOHandler
import utils.WinningGame

internal class StateTest {
    @Test
    fun `player balance after win is bigger then before`() {
        val player = Player(100)
        var beforeGameBalance = player.balance
        WinningGame(Dealer(), player, SimpleIOHandler()).run()
        assertTrue(player.balance > beforeGameBalance)

        beforeGameBalance = player.balance
        WinningGame(Dealer(), player, SimpleIOHandler(), true).run()
        assertTrue(player.balance > beforeGameBalance)
    }

    @Test
    fun `player balance after lost is lower then before`() {
        val player = Player(100)
        val beforeGameBalance = player.balance
        LostGame(Dealer(), player, SimpleIOHandler()).run()
        assertTrue(player.balance < beforeGameBalance)
    }

    @Test
    fun `player balance after draw is the same`() {
        val player = Player(100)
        val beforeGameBalance = player.balance
        DrawGame(Dealer(), player, SimpleIOHandler()).run()
        assertEquals(player.balance, beforeGameBalance)
    }

    @Test
    fun `State from one game is equals to state from another`() {
        assertEquals(
            State.GameOver(DrawGame(Dealer(), Player(1000), SimpleIOHandler())),
            State.GameOver(WinningGame(Dealer(), Player(1000), SimpleIOHandler()))
        )
    }

    @Test
    fun `State not equals to StateAction`() {
        val game = DrawGame(Dealer(), Player(1000), SimpleIOHandler())
        assertNotEquals(
            State.GameOver(game),
            PlayersMoveAction(game)
        )
    }
}
