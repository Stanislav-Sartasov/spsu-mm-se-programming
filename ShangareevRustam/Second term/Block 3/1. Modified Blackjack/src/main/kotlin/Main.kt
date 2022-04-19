import blackjackPlayer.BlackjackPlayer
import blackjackTable.BlackjackTable
import botLoader.BotLoader

fun main() {
	val botEasyFilePath = "file:///lib/botEasy/src/main/kotlin/botEasy/"
	val botEasyImportPath = "botEasy.BotEasy"
	val botMediumFilePath = botEasyFilePath.replace("Easy", "Medium")
	val botMediumImportPath = botEasyImportPath.replace("Easy", "Medium")
	val botHardFilePath = botEasyFilePath.replace("Easy", "Hard")
	val botHardImportPath = botEasyImportPath.replace("Easy", "Hard")
	val classes = BotLoader.load(
		listOf(
			Pair(botEasyFilePath, botEasyImportPath),
			Pair(botMediumFilePath, botMediumImportPath),
			Pair(botHardFilePath, botHardImportPath)
		)
	)
	val objects = BotLoader.getClassObjects(classes)

	val weakPlayer: BlackjackPlayer
	val averagePlayer: BlackjackPlayer
	val strongPlayer: BlackjackPlayer
	if (objects.size == 3 && objects.all { it is BlackjackPlayer }) {
		weakPlayer = objects[0] as BlackjackPlayer
		averagePlayer = objects[1] as BlackjackPlayer
		strongPlayer = objects[2] as BlackjackPlayer
	} else {
		throw Exception("An error has occurred! Some of the bots were not loaded or were loaded incorrectly")
	}

	val numbOfTests = 100
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

	val players = listOf(weakPlayer, averagePlayer, strongPlayer)
	newTable.playBlackjackAndPrintResult(players, 100)
	println("Player No.1 = easy bot, Player No.2 = medium bot, Player No.3 = hard bot")
}