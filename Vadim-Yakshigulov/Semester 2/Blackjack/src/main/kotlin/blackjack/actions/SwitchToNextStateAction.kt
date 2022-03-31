package blackjack.actions

import blackjack.classes.State
import blackjack.interfaces.IGame
import blackjack.interfaces.IStateAction

class SwitchToNextStateAction(override val game: IGame, private val nextState: State) : IStateAction {
    override fun execute(): State {
        return nextState
    }
}

