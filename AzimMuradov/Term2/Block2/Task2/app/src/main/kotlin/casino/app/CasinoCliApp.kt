package casino.app

import casino.lib.blackjack.PlayerStrategy
import casino.lib.blackjack.Table
import casino.lib.blackjack.bots.basic.BasicStrategy
import casino.lib.blackjack.bots.hilo.HiLoStrategy
import casino.lib.blackjack.bots.simple.SimpleStrategy

object CasinoCliApp {

    fun run() {
        println("Average of $averageN runs:")
        for (strategy in listOf(SimpleStrategy, BasicStrategy, HiLoStrategy)) {
            val resBankroll = playSessionAverage(
                table = Table.standard(),
                strategy = strategy,
                bankroll = bankroll,
                averageN = averageN
            )
            println("- ${strategy::class.simpleName} : score = $resBankroll / $bankroll")
        }
    }


    private const val bankroll = 5000u

    private const val averageN = 1000

    private fun playSessionAverage(
        table: Table, strategy: PlayerStrategy, bankroll: UInt,
        averageN: Int,
    ): Double = sequence {
        repeat(times = averageN) {
            val (newBankroll, _) = table.playSession(strategy, bankroll)
            yield(newBankroll)
        }
    }.map(UInt::toInt).average()
}
