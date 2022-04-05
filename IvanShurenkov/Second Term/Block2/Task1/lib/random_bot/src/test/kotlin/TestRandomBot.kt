import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import lib.blackjack.base.*
import lib.blackjack.base.state.PlayerMove.*
import lib.blackjack.base.state.GameInfo
import lib.blackjack.base.state.PlayerMove
import lib.blackjack.bots.RandomBot

class TestRandomBot {
    @Test
    fun `Test RandomBot bet`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        assertEquals(1u, RandomBot.getBet(10u, gameInfo))
    }

    @Test
    fun `Test RandomBot move`() {
        val gameInfo = GameInfo(1u..10u, 1u)
        var moves: List<PlayerMove> = emptyList()
        for (i in 1..14) {
            moves = moves + RandomBot.getMove(Hand(), gameInfo)
        }
        assert(HIT in moves)
        assert(STAND in moves)
    }
}