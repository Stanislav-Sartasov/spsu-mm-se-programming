package blackjack.classes

import blackjack.player.Player
import botLib.Bot
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class BotTest {
    @Test
    fun `adding new strategy adds corresponds map keys and values`() {
        val b = object : Bot(Player(100)) {
            override val strategy: Map<Pair<Int, Int>, String> = mutableMapOf<Pair<Int, Int>, String>()
                .addStrategy(1..10, 1..10, "Stand")
                .addStrategy(11..20, 5..5, "Hit")
        }
        for (i in 1..10)
            for (j in 1..10)
                assertEquals(b.strategy[Pair(i, j)], "Stand")

        for (i in 11..20)
            assertEquals(b.strategy[Pair(i, 5)], "Hit")
    }
}