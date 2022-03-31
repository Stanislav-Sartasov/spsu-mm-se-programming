package blackjack.actions

import blackjack.actions.inGame.DoubleAction
import blackjack.actions.inGame.HitAction
import blackjack.actions.inGame.SplitAction
import blackjack.actions.inGame.StandAction
import blackjack.classes.Hand
import blackjack.classes.ScoreCounter
import blackjack.classes.State
import blackjack.interfaces.IGame
import blackjack.interfaces.IInGameAction
import blackjack.interfaces.IPlayer
import blackjack.interfaces.IStateAction

class PlayersMoveAction(override val game: IGame) : IStateAction {

    private fun canUseSplitAction(activeHand: Hand, player: IPlayer): Boolean {
        return activeHand.cards.size == 2 && activeHand.bet <= player.balance && ScoreCounter.calculateForCard(
            activeHand.cards.first()
        ) == ScoreCounter.calculateForCard(activeHand.cards.last())
    }

    private fun canUseDoubleAction(activeHand: Hand, player: IPlayer): Boolean {
        return player.hands.size == 1 && activeHand.cards.size == 2 && activeHand.bet <= player.balance
    }

    private fun getAllowedActions(): Map<String, IInGameAction> {
        val allowedActions: MutableList<IInGameAction> =
            mutableListOf(HitAction(game, game.player.activeHand), StandAction(game, game.player.activeHand))

        if (canUseSplitAction(game.player.activeHand, game.player)) allowedActions.add(
            SplitAction(game, game.player.activeHand)
        )

        if (canUseDoubleAction(game.player.activeHand, game.player)) allowedActions.add(
            DoubleAction(game, game.player.activeHand)
        )

        return allowedActions.associateBy { it.displayName }
    }

    override fun execute(): State {
        game.player.activeHand =
            game.ioHandler.chooseFromHands(
                "Choose desired active hand",
                game.player.activeHand,
                *game.player.hands.filter { !it.blocked }.toTypedArray(),
            )

        val allowedActions = getAllowedActions()
        val chosenActionDisplayName =
            game.ioHandler.chooseFromPossibleActions("What you want to do?", *allowedActions.keys.toTypedArray())
        val chosenAction = allowedActions[chosenActionDisplayName]!!

        chosenAction.execute()

        if (ScoreCounter.calculateForHand(game.player.activeHand) > 21)
            StandAction(game, game.player.activeHand).execute()

        if (game.player.hands.all { it.blocked })
            return State.DealerTurn(game)

        return State.PlayerTurn(game)
    }
}