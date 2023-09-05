package ui.mvi

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.launch

abstract class AbstractStore<Intent, State>(
    val scope: CoroutineScope
) : Store<Intent, State> {
    protected val intentChannel: Channel<Intent> = Channel(Channel.UNLIMITED)
    abstract override val stateFlow: StateFlow<State>

    override fun send(intent: Intent) {
        scope.launch { intentChannel.send(intent) }
    }

    protected abstract suspend fun fold(curState: ChatState, intent: Intent): ChatState
}