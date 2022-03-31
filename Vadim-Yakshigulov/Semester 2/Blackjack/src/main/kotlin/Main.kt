import blackjack.classes.Player
import blackjack.classes.StatisticCollector
import bots.*

fun main() {

    var chosenBetSize: Int
    do {
        println("Choose start bet size >= 10 to play Blackjack game: ")
        chosenBetSize = try {
            readLine()?.toInt() ?: 0
        } catch (e: Exception) {
            0
        }
    } while (chosenBetSize < 10)

    val player = Player(chosenBetSize)
    val namesToBots = listOf(
        DealerLikeBlackjackBot(player),
        RandomizedBlackjackBot(player),
        StrategyBasedBlackjackBot(player)
    ).associateBy { it.javaClass.simpleName }

    var chosenBot: String
    do {
        println("Choose bot to play Blackjack game: ")
        println(namesToBots.keys)
        chosenBot = try {
            readLine() ?: ""
        } catch (e: Exception) {
            ""
        }
    } while (chosenBot !in namesToBots)


    var count: Int
    do {
        println("How many times you wanna play?")
        count = try {
            readLine()?.toInt() ?: 0
        } catch (e: Exception) {
            -1
        }
    } while (count <= 0)

    namesToBots[chosenBot]!!.run(count)

    StatisticCollector.printFullStatisticToConsole()
    StatisticCollector.reset()
}