package player

import cash_account.Account
import game.roulette.session.Session

abstract class Player(
	val name: String,
	val account: Account
) {
	abstract fun makeBet(session: Session)
}