package lib.blackjack.base

import lib.blackjack.base.state.PlayerMove.HIT
import lib.blackjack.base.state.GameInfo

class GameTable(_gameInfo: GameInfo) {
    private val baseWin: Double = 2.0
    private val winWithBJ: Double = 2.5

    private val deck: Deck
    var gameInfo: GameInfo
        private set
    val croupierHand: Hand = Hand()

    init {
        gameInfo = _gameInfo
        deck = Deck(gameInfo.cntDecks)
        deck.shaffle()
    }

    private fun addCard(hand: Hand): Card {
        val card = deck.getCard()
        hand.addCard(card)
        gameInfo.addCard(card)
        return card
    }

    fun playSession(players: List<IPlayer>, bankrolls: Array<UInt>, cntOfGames: Int): Int {
        var casinoWin = 0
        if (players.size !in 1..6) {
            println("Count of players must be from 1 to 6")
            return 0
        }
        val hands: List<Hand> = List(players.size) { Hand() }
        for (numGame in 1..cntOfGames) {
            if (deck.remain() < gameInfo.cntDecks.toInt() * 13) {
                deck.shaffle()
                gameInfo.clearDealtCards()
            }

            //bets
            val bets: Array<UInt> = Array(players.size) { 0u }
            for (i in players.indices) {
                val bet: UInt = players[i].getBet(bankrolls[i], gameInfo)
                if (bet in gameInfo.rangeBet && bet <= bankrolls[i]) {
                    bankrolls[i] -= bet
                    bets[i] = bet
                }
            }

            //distribution
            for (i in players.indices) {
                addCard(hands[i])
                addCard(hands[i])
                //println("${players[i].name} take a ${addCard(hands[i]).value}")
            }
            gameInfo.croupierCard = addCard(croupierHand)
            // println("Croupier take a ${gameInfo.croupierCard.value}")

            //moves
            for (i in players.indices) {
                if (bets[i] == 0u)
                    continue
                var move = players[i].getMove(hands[i], gameInfo)
                while (move == HIT) {
                    addCard(hands[i])
                    //println("${players[i].name} take a ${addCard(hands[i]).value}")
                    if (21 <= hands[i].minScore())
                        break
                    move = players[i].getMove(hands[i], gameInfo)
                }
            }

            while (croupierHand.maxScore() < 17)
                addCard(croupierHand)
            //println("Croupier take a ${addCard(croupierHand).value}")

            //check
            for (i in players.indices) {
                val score = hands[i].maxScore()
                val croupierScore = croupierHand.maxScore()
                if (score !in 1..21 || croupierScore in 1..21 && score < croupierScore
                    || !hands[i].hasBlackjack() && croupierHand.hasBlackjack()
                ) {
                    //lose
                    casinoWin += bets[i].toInt()
                } else if (score == croupierScore && hands[i].hasBlackjack() == croupierHand.hasBlackjack()) {
                    //draw
                    bankrolls[i] += bets[i]
                } else if (croupierScore !in 1..21 || score > croupierScore && !hands[i].hasBlackjack()) {
                    //base win
                    casinoWin -= bets[i].toInt()
                    bankrolls[i] += bets[i] * baseWin.toUInt()
                } else if (hands[i].hasBlackjack() && !croupierHand.hasBlackjack()) {
                    //win with blackjack
                    casinoWin -= (bets[i].toDouble() * winWithBJ).toInt() - bets[i].toInt()
                    bankrolls[i] += (bets[i].toDouble() * winWithBJ).toUInt()
                }
            }
        }
        return casinoWin
    }
}