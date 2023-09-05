package ui

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material.Button
import androidx.compose.material.Surface
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Dialog
import androidx.compose.ui.window.WindowPosition
import androidx.compose.ui.window.rememberDialogState
import ui.mvi.ChatIntent
import ui.mvi.ChatState

@Composable
internal inline fun Error() {
    Dialog(
        onCloseRequest = { store.send(ChatIntent.OnError) },
        state = rememberDialogState(
            position = WindowPosition(Alignment.Center),
            width = 300.dp,
            height = 200.dp
        )
    ) {
        val state = State.current as ChatState.Error
        Surface(Modifier.fillMaxSize()) {
            Column {
                Text(state.error.toString())
                Button(onClick = { store.send(ChatIntent.OnError) }, content = { Text("Back") })
            }
        }
    }
}