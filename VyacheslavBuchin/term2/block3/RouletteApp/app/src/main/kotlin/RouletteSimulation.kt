import bot.BotPlayer
import cash_account.CashAccount
import game.roulette.session.RouletteSession
import game.roulette.wheel.EuropeanRouletteWheel

class RouletteSimulation(
	private val gameCount: Int,
	initCasinoBalance: Double,
	private val bots: List<BotPlayer>
) {
	private val session = RouletteSession(EuropeanRouletteWheel(), CashAccount(initCasinoBalance))

	fun simulate() {
		for (i in 1..gameCount) {
			bots.forEach { it.makeBet(session) }
			session.play()
		}
	}

	fun getReport(): Map<String, Double> {
		val report = mutableMapOf<String, Double>()
		bots.forEach {
			report[it.name] = it.account.balance()
		}
		return report
	}
}