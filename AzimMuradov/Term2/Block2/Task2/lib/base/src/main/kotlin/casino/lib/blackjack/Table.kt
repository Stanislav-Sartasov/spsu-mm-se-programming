package casino.lib.blackjack

import casino.lib.blackjack.BlackJackConstants.BJ
import casino.lib.blackjack.BlackJackConstants.BJ_WIN_MULTIPLIER
import casino.lib.blackjack.BlackJackConstants.WIN_MULTIPLIER
import casino.lib.card.Card
import casino.lib.shoe.Shoe
import casino.lib.shoe.ShoesFabric
import kotlin.math.sign

class Table private constructor(val info: TableInfo, private val shoeFabric: (numberOfDecks: Int) -> Shoe) {

    fun playSession(strategy: PlayerStrategy, bankroll: UInt): Pair<UInt, List<GameState.Ended>> {
        var newBankroll = bankroll
        val shoe = shoeFabric(info.numberOfDecks)
        val gameResults = buildList {
            repeat(times = 5 * info.numberOfDecks) {
                if (newBankroll > 0u) {
                    val bet = strategy.getNextBet(
                        playerBankroll = newBankroll,
                        gameState = GameState.BeforeGame(info, shoe.dealt)
                    )
                    require(bet <= newBankroll) { "Player's bet must not exceed his bankroll" }

                    val gameResult = play(strategy, bet, shoe)

                    add(gameResult)

                    newBankroll += gameResult.amount - bet
                }
            }
        }

        return newBankroll to gameResults
    }


    private fun play(strategy: PlayerStrategy, bet: UInt, shoe: Shoe): GameState.Ended {
        require(bet in info.allowedBets) { "Player's bet must be in the 'allowed bets' range" }

        val (initDealer, initPlayer) = shoe.dealInitCards()

        if (initPlayer.isBJ()) {
            return cmpHandsAndGetEndState(dealer = initDealer.hand, player = initPlayer, bet)
        }

        val states = generateSequence<GameState>(
            seed = GameState.InGame(
                table = info,
                dealer = DealerState(initDealer.openCard),
                player = PlayerState(initPlayer),
                discard = shoe.publiclyDealt
            )
        ) { state ->
            when (state) {
                is GameState.InGame -> strategy.getNextMove(state).applyMove(
                    gameHands = GameHands(initDealer, player = state.player.hand),
                    bet = bet,
                    shoe = shoe
                )
                is GameState.Ended -> null
                is GameState.BeforeGame -> error("impossible")
            }
        }

        return states.last() as GameState.Ended
    }

    private fun PlayerMove.applyMove(gameHands: GameHands, bet: UInt, shoe: Shoe): GameState {
        val (initDealer, player) = gameHands

        return when (this) {
            PlayerMove.HIT -> {
                val newPlayer = Hand(cards = player.cards + shoe.dealCard())
                when {
                    newPlayer.isBust() -> GameState.Ended.Lost
                    newPlayer.total() == BJ -> endGame(
                        gameHands = GameHands(initDealer, player = newPlayer),
                        shoe = shoe,
                        bet = bet
                    )
                    else -> GameState.InGame(
                        table = info,
                        dealer = DealerState(initDealer.openCard),
                        player = PlayerState(newPlayer),
                        discard = shoe.publiclyDealt
                    )
                }
            }
            PlayerMove.STAND -> endGame(gameHands, shoe, bet)
        }
    }


    companion object {

        fun standard(): Table = custom()

        fun custom(
            info: TableInfo = TableInfo(numberOfDecks = 8, allowedBets = 1u..5000u),
            shoeFabric: (numberOfDecks: Int) -> Shoe = ShoesFabric::shuffled,
        ): Table = Table(
            info = info,
            shoeFabric = shoeFabric
        )


        // Private utilities

        private data class GameHands(val initDealer: InitDealerHand, val player: Hand)

        private data class InitDealerHand(val openCard: Card, val holeCard: Card) {

            val hand = Hand(listOf(openCard, holeCard))
        }


        private val Shoe.publiclyDealt get() = dealt.toMutableList().apply { removeAt(index = 3) }.toList()

        private fun Shoe.dealInitCards(): GameHands {
            val cards = List(size = 2 + 2) { dealCard() }
            return GameHands(
                initDealer = InitDealerHand(openCard = cards[1], holeCard = cards[3]),
                player = Hand(listOf(cards[0], cards[2]))
            )
        }

        private fun endGame(gameHands: GameHands, shoe: Shoe, bet: UInt): GameState.Ended {
            val (dealerHand, playerHand) = gameHands
            val finalDealerHand = Hand(
                cards = buildList {
                    addAll(dealerHand.hand.cards)
                    while (Hand(cards = this).total() < 17) add(shoe.dealCard())
                }
            )
            return cmpHandsAndGetEndState(finalDealerHand, playerHand, bet)
        }

        private fun cmpHandsAndGetEndState(dealer: Hand, player: Hand, bet: UInt): GameState.Ended =
            when (player.compareTo(dealer).sign) {
                +1 -> GameState.Ended.Won(amount = player.calcWin(bet))
                0 -> GameState.Ended.Push(amount = bet)
                else -> GameState.Ended.Lost
            }

        private fun Hand.calcWin(bet: UInt) =
            (bet.toDouble() * if (isBJ()) BJ_WIN_MULTIPLIER else WIN_MULTIPLIER).toUInt()
    }
}
