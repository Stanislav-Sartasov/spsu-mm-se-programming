package ui.mvi

import kotlinx.coroutines.flow.StateFlow

interface Store<Intent, State> {
    fun send(intent: Intent)
    val stateFlow: StateFlow<State>
}