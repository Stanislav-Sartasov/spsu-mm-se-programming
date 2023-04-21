package roulette

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import roulette.bets.TestPlayer

internal class GameTest {
    val game = Game()
    val firstPlayer = TestPlayer("1", 100)
    val secondPlayer = TestPlayer("2", 200)

    @Test
    fun addPlayer() {
        game.addPlayer(firstPlayer)
        game.addPlayer(secondPlayer)
        assertEquals(game.players, arrayListOf(firstPlayer, secondPlayer))
    }

    @Test
    fun removePlayer() {
        game.addPlayer(firstPlayer)
        game.addPlayer(secondPlayer)
        game.removePlayer("1")
        assertEquals(game.players, arrayListOf<APlayer>(secondPlayer))
    }

    @Test
    fun play() {
        game.addPlayer(firstPlayer)
        game.play()
        kotlin.test.assertNotEquals(game.players.first().balance, 100)
    }
}
