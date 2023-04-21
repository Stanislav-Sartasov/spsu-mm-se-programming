package bet

interface Bet {

	fun isWon(value: Int): Boolean

	fun wonValuesCount(): Int

}