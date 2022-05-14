package player

import cash_account.Account
import game.roulette.session.Session

abstract class Player(
	open val name: String,
	open val account: Account
) {
	abstract fun makeBet(session: Session)
}