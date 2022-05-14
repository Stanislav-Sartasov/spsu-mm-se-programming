import bot.BotPlayer

class RouletteApp(
	private val simulationCount: Int,
	private val bots: List<BotPlayer>,
	private val initBotBalance: Double
) {
	private val gameStats = mutableMapOf<String, Double>()
	fun run() {
		for (i in 1..simulationCount) {
			bots.forEach { it.account.setMoney(initBotBalance) }
			val simulation = RouletteSimulation(GAME_COUNT, INIT_CASINO_BALANCE, bots)
			simulation.simulate()
			val report = simulation.getReport()
			report.forEach {
				gameStats[it.key] = gameStats[it.key]?.plus(it.value) ?: it.value
			}
		}
		printConclusion()
	}

	private fun printConclusion() {
		println("After $simulationCount simulations, bots have in average:")
		gameStats.forEach {
			val botName = it.key
			val endBalance = it.value / simulationCount
			println("$botName - $endBalance$")
		}
	}
}