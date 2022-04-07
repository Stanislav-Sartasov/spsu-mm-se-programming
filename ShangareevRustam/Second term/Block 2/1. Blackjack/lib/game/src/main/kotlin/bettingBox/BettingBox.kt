package bettingBox

import blackjackHand.*
import blackjackPlayer.*

data class BettingBox(val player: BlackjackPlayer) {
	val bet: Int = player.getBet()
	val playersHand: BlackjackHand = BlackjackHand()
}