package blackjack.action

import blackjack.state.State
import blackjack.game.IGame

interface IStateAction {
    val game: IGame
    fun execute(): State
}