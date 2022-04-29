const val INIT_CASINO_BALANCE = 2281337.0
const val INIT_BOT_BALANCE = 1000.0
const val GAME_COUNT = 40
const val SIMULATION_COUNT = 10000
const val PREAMBLE = "This program simulates $SIMULATION_COUNT times $GAME_COUNT roulette games of 4 bots\n" +
		"and prints average amount of money left\n" +
		"Every bot has $INIT_BOT_BALANCE$, casino has $INIT_CASINO_BALANCE$ at the beginning\n" +
		"Bot 1 uses martingale strategy\n" +
		"Bot 2 uses anti-martingale strategy\n" +
		"Bot 3 bets fixed amount on even every time\n" +
		"Bot 4 bets fixed amount on zero every time\n"

fun main(args: Array<String>) {
	println(PREAMBLE)
	RouletteApp(SIMULATION_COUNT).run()
}