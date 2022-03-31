package casino.app

import casino.lib.blackjack.PlayerStrategy
import casino.lib.blackjack.Table

object CasinoCliApp {

    fun run(args: Array<String>) {
        if (args.isEmpty()) {
            println("No arguments were given.")
            return
        }

        val strategies = args.flatMap(BotsLoader::loadAllBots)

        if (strategies.isEmpty()) {
            println("No bots were found.")
        } else {
            println("${strategies.size} bots were found.")

            println("Average of $averageN runs:")
            for (strategy in strategies) {
                val (initBankroll, resBankroll) = playSessionAverage(
                    table = Table.standard(),
                    strategy = strategy,
                    bankroll = bankroll,
                    averageN = averageN
                )
                println("${strategy::class.simpleName} : score = $initBankroll / $resBankroll")
            }
        }
    }


    private const val averageN = 1000

    private const val bankroll = 5000u

    private fun playSessionAverage(
        table: Table, strategy: PlayerStrategy, bankroll: UInt,
        averageN: Int,
    ): Pair<Double, Int> = sequence {
        repeat(times = averageN) {
            val (newBankroll, _) = table.playSession(strategy, bankroll)
            yield(newBankroll)
        }
    }.map(UInt::toInt).average() to bankroll.toInt()
}
