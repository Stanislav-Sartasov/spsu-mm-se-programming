package bet

class StraightBet(private val value: Int) : Bet {

	override fun isWon(value: Int) = value == this.value

	override fun wonValuesCount() = 1

}