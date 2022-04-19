import blackjackHand.BlackjackHand
import blackjackPlayer.BlackjackPlayer
import botEasy.BotEasy
import botHard.BotHard
import botLoader.BotLoader
import botMedium.BotMedium
import card.Card
import cardSuit.CardSuit
import cardValue.CardValue
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import kotlin.random.Random

class BotLoaderTests {

	@Test
	fun loadTest() {
		val paths = listOf(
			Pair("file:///lib/game/src/main/kotlin/bettingBox/", "bettingBox.BettingBox"),
			Pair("file:///lib/game/src/main/kotlin/blackjackHand/", "blackjackHand.BlackjackHand"),
			Pair("file:///lib/game/src/main/kotlin/blackjackPlayer/", "blackjackPlayer.BlackjackPlayer"),
			Pair("file:///lib/game/src/main/kotlin/blackjackTable/", "blackjackTable.BlackjackTable"),
			Pair("file:///lib/game/src/main/kotlin/card/", "card.Card"),
			Pair("file:///lib/game/src/main/kotlin/cardSuit/", "cardSuit.CardSuit"),
			Pair("file:///lib/game/src/main/kotlin/cardValue/", "cardValue.CardValue"),
			Pair("file:///lib/game/src/main/kotlin/dealer/", "dealer.Dealer"),
			Pair("file:///lib/game/src/main/kotlin/decision/", "decision.Decision"),
			Pair("file:///lib/game/src/main/kotlin/gameResult/", "gameResult.GameResult"),
			Pair("file:///lib/game/src/main/kotlin/shoe/", "shoe.Shoe"),
			Pair("file:///lib/game/src/main/kotlin/testPlayer/", "testPlayer.TestPlayer"),
			Pair("file:///lib/botEasy/src/main/kotlin/botEasy/", "botEasy.BotEasy"),
			Pair("file:///lib/botMedium/src/main/kotlin/botMedium/", "botMedium.BotMedium"),
			Pair("file:///lib/botHard/src/main/kotlin/botHard/", "botHard.BotHard")
		)

		var flag = true
		try {
			val list = BotLoader.load(paths)
		}
		catch (e: Exception) {
			flag = false
		}

		assert(flag)
	}

	@Test
	fun getClassObjectsTest() {
		val botEasyFilePath = "file:///lib/botEasy/src/main/kotlin/botEasy/"
		val botEasyImportPath = "botEasy.BotEasy"
		val botMediumFilePath = "file:///lib/botMedium/src/main/kotlin/botMedium/"
		val botMediumImportPath = "botMedium.BotMedium"
		val botHardFilePath = "file:///lib/botHard/src/main/kotlin/botHard/"
		val botHardImportPath = "botHard.BotHard"
		val classes = BotLoader.load(
			listOf(
				Pair(botEasyFilePath, botEasyImportPath),
				Pair(botMediumFilePath, botMediumImportPath),
				Pair(botHardFilePath, botHardImportPath)
			)
		)
		val objects = BotLoader.getClassObjects(classes).map {it -> it as BlackjackPlayer}

		val cards = List(Random.nextInt(2, 4)) {
			Card(CardValue.values()[Random.nextInt(13)], CardSuit.values()[Random.nextInt(4)])
		}
		val dealerFaceUpCard = Card(CardValue.values()[Random.nextInt(13)], CardSuit.values()[Random.nextInt(4)])
		val hand = BlackjackHand()
		for (card in cards) hand.dealCard(card)

		assertEquals(
			objects.map {it -> it.getDecision(hand, dealerFaceUpCard)},
			listOf(BotEasy(), BotMedium(), BotHard()).map {it -> it.getDecision(hand, dealerFaceUpCard)}
		)
	}
}