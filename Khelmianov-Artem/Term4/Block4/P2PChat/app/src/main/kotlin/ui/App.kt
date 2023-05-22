package ui

import androidx.compose.desktop.ui.tooling.preview.Preview
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material.MaterialTheme
import androidx.compose.material.Surface
import androidx.compose.material.darkColors
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.DpSize
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.WindowState
import androidx.compose.ui.window.application
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.SupervisorJob
import ui.mvi.ChatIntent
import ui.mvi.ChatState
import ui.mvi.createChatStore


internal val store = CoroutineScope(SupervisorJob()).createChatStore()
internal val State = compositionLocalOf<ChatState> { error("") }

fun main() = application {
    val state by store.stateFlow.collectAsState()
    Window(
        onCloseRequest = {
            store.send(ChatIntent.OnClose)
            while (store.stateFlow.value != ChatState.Closed) {
            }
            exitApplication()
        },
        title = "Chat",
        state = WindowState(size = DpSize(800.dp, 550.dp)),
    ) {
        CompositionLocalProvider(State provides state) {
            MaterialTheme(colors = darkColors()) {
                App()
            }
        }
    }
}


@Preview
@Composable
internal inline fun App() = Surface {
    when (val state = State.current) {
        is ChatState.Disconnected -> Login()
        is ChatState.Connected -> Chat()
        is ChatState.Error -> Error()
        else -> {}
    }
}

