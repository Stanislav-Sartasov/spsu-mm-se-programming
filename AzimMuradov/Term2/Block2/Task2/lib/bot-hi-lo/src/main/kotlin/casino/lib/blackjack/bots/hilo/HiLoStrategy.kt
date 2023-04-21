package casino.lib.blackjack.bots.hilo

import casino.lib.blackjack.*
import casino.lib.blackjack.bots.basic.BasicStrategy
import casino.lib.blackjack.states.BeforeGameState
import casino.lib.blackjack.states.GameState
import casino.lib.card.*

object HiLoStrategy : PlayerStrategy {

    override fun getNextBet(playerBankroll: UInt, gameState: BeforeGameState): UInt {
        val (table, dealt) = gameState
        val tc = trueCount(
            runningCount = runningCount(cards = dealt),
            seenCardsSize = dealt.size,
            numberOfDecks = table.numberOfDecks
        )
        val betUnit = BasicStrategy.getNextBet(playerBankroll, gameState)

        return when (tc) {
            in 1..Int.MAX_VALUE -> betUnit * (tc + 1).toUInt()
            else -> betUnit
        }.coerceIn(table.allowedBets)
    }

    override fun getNextMove(gameState: GameState.InGame): PlayerMove {
        val (table, dealer, player, dealt) = gameState

        val rc = runningCount(cards = dealt)
        val tc = trueCount(runningCount = rc, seenCardsSize = dealt.size, numberOfDecks = table.numberOfDecks)

        val total = player.hand.total()
        val i = dealerIndex[dealer.openCard.rank]

        val deviate = if (player.hand.isSoft()) {
            when (total) {
                19 -> tc >= 3 && i == 2 || tc >= 1 && i in 3..4
                17 -> tc >= 1 && i == 0
                else -> false
            }
        } else {
            when (total) {
                16 -> tc >= 4 && i == 7 || rc > 0 && i == 8
                15 -> tc >= 4 && i == 8
                13 -> tc <= -1 && i == 0
                12 -> tc >= 3 && i == 0 || tc >= 2 && i == 1 || rc < 0 && i == 2
                11 -> tc >= 1 && i == 9
                10 -> tc >= 4 && i in 8..9
                9 -> tc >= 1 && i == 0 || tc >= 3 && i == 5
                8 -> tc >= 2 && i == 4
                else -> false
            }
        }

        val basicStrategyMove = BasicStrategy.getNextMove(gameState)

        return if (deviate) basicStrategyMove.invert() else basicStrategyMove
    }


    private fun runningCount(cards: List<Card>): Int = cards.map {
        when (it.rank) {
            in CardRank.TWO..CardRank.SIX -> +1
            in CardRank.SEVEN..CardRank.NINE -> 0
            else -> -1
        }
    }.sum()

    private fun trueCount(runningCount: Int, seenCardsSize: Int, numberOfDecks: Int): Int {
        val allDecksSize = Card.deck.size * numberOfDecks
        return runningCount * allDecksSize / (numberOfDecks * (allDecksSize - seenCardsSize))
    }

    private val dealerIndex = buildMap {
        put(CardRank.ACE, 9)
        putAll(CardRank.values().filter(CardRank::isNumeral).associateWith { it.ordinal - 1 })
        putAll(CardRank.values().filter(CardRank::isFace).associateWith { 8 })
    }
}
