package blackjack.interfaces

import blackjack.classes.State

interface IStateAction {
    val game: IGame
    fun execute(): State
}