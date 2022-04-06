import bet.OutsideRouletteBet
import bet.StraightBet
import bot.BotPlayer
import bot.bet_strategy.*
import cash_account.CashAccount
import game.roulette.session.RouletteSession
import game.roulette.wheel.EuropeanRouletteWheel

class RouletteSimulation(
	private val gameCount: Int,
	initCasinoBalance: Double,
	initBotBalance: Double
) {
	private val initialBet = 10.0
	private val session = RouletteSession(EuropeanRouletteWheel(), CashAccount(initCasinoBalance))
	private val bots = arrayOf(
		BotPlayer(
			"Bot 1: Martingale strategy",
			CashAccount(initBotBalance),
			MartingaleBetStrategy(initialBet)
		),
		BotPlayer(
			"Bot 2: Anti-Martingale strategy",
			CashAccount(initBotBalance),
			AntiMartingaleBetStrategy(initialBet)
		),
		BotPlayer(
			"Bot 3: Only even bets",
			CashAccount(initBotBalance),
			FixedAmountBetStrategy(OutsideRouletteBet.EVEN, initialBet)
		),
		BotPlayer(
			"Bot 4: Only zero bets",
			CashAccount(initBotBalance),
			FixedAmountBetStrategy(StraightBet(0), initialBet)
		)
	)

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