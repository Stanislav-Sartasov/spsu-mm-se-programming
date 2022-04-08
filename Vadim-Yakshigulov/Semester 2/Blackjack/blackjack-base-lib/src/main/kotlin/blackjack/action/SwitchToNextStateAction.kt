package blackjack.action

import blackjack.state.State
import blackjack.game.IGame

class SwitchToNextStateAction(override val game: IGame, private val nextState: State) : IStateAction {
    override fun execute(): State {
        return nextState
    }
}

