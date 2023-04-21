package casino.app

import casino.lib.blackjack.PlayerStrategy
import casino.lib.blackjack.Table

object CasinoCliApp {

    fun run(args: Array<String>) {
        if (args.isEmpty()) {
            println("No arguments were given.")
            return
        }

        val strategies = args.flatMap(::loadBotsRecursively)

        if (strategies.isEmpty()) {
            println("No bots were found.")
            return
        }


        println("${strategies.size} bots were found.")

        println("Average of $averageN runs:")

        for (strategy in strategies) {
            try {
                val resBankroll = playSessionAverage(
                    table = Table.standard(),
                    strategy = strategy,
                    bankroll = bankroll,
                    averageN = averageN
                )
                println("- ${strategy::class.simpleName} : score = $resBankroll / $bankroll")
            } catch (e: Throwable) {
                println("- ${strategy::class.simpleName} : error \"${e::class.qualifiedName}: ${e.message}\"")
            }
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


    private fun loadBotsRecursively(path: String): List<PlayerStrategy> = BotLoader.run {
        findJarFilesRecursively(path).flatMap(::loadBotsFromJarFile)
    }
}
