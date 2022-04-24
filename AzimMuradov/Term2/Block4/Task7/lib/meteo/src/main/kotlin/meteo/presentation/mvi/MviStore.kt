package meteo.presentation.mvi

import kotlinx.coroutines.flow.StateFlow

interface MviStore<in Wish, out State> {

    val state: StateFlow<State>

    fun consume(wish: Wish)
}
