import blackjackTable.BlackjackTable
import botEasy.*
import botMedium.*
import botHard.*

fun main() {

	val numbOfTests = 100;
	val numbOfGames = 40

	println("This program demonstrates the operation of a blackjack program")
	println(
		"It implements 3 bots with 3 strategies of varying complexity," +
				" which will be tested in practice:"
	)
	println(
		"we will find out how much money" +
				" each bot has on average after $numbOfGames bets,"
	)
	println(
		"having conducted $numbOfTests games of $numbOfGames bets of \$ 10" +
				" with an initial amount of \$ 1000 for each bot"
	)

	val newTable = BlackjackTable()
	val weakPlayer = BotEasy()
	val averagePlayer = BotMedium()
	val strongPlayer = BotHard()

	val players = listOf(weakPlayer, averagePlayer, strongPlayer)
	newTable.playBlackjackAndPrintResult(players, 100)
	println("Player No.1 = easy bot, Player No.2 = medium bot, Player No.3 = hard bot")
}