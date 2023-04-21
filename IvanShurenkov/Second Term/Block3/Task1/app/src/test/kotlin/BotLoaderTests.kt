import lib.blackjack.base.IPlayer
import lib.blackjack.bots.BaseBot
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFails

class BotLoaderTests {
    @Test
    fun `Test load Base bot`() {
        val bots: List<IPlayer> =
            BotLoader().loadBots(listOf("src/test/resources/jars/base_bot.jar"))
        if (bots.isEmpty()) {
            assertFails { "BaseBot doesn't load" }
            return
        }
        val baseBot = BaseBot
        assertEquals(baseBot, bots[0])
    }

    @Test
    fun `Jar file doesn't exist`() {
        val bots: List<IPlayer> =
            BotLoader().loadBots(listOf("src/test/resources/jars/base_bot1.jar"))
        assert(bots.isEmpty())
    }

    @Test
    fun `Jar file hasn't IPlayer`() {
        val bots: List<IPlayer> =
            BotLoader().loadBots(listOf("src/test/resources/jars/without_IPlayer.jar"))
        assert(bots.isEmpty())
    }
}