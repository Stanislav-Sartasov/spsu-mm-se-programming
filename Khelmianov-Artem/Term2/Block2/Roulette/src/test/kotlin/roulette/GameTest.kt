package roulette

import bots.RndDozenPlayer
import bots.RndNumberPlayer
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotEquals

internal class GameTest {
    val game = Game()
    val firstPlayer = RndDozenPlayer("1", 100)
    val secondPlayer = RndNumberPlayer("2", 200)

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
        assertNotEquals(game.players.first().balance, 100)
    }
}