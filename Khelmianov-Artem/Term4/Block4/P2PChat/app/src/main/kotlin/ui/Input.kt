package ui

import androidx.compose.desktop.ui.tooling.preview.Preview
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Send
import androidx.compose.runtime.*
import androidx.compose.ui.ExperimentalComposeUiApi
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.key.Key
import androidx.compose.ui.input.key.isCtrlPressed
import androidx.compose.ui.input.key.key
import androidx.compose.ui.input.key.onPreviewKeyEvent
import ui.mvi.ChatIntent

@OptIn(ExperimentalComposeUiApi::class)
private fun Modifier.onCtrlEnter(action: () -> Unit) = this.onPreviewKeyEvent {
    if (it.key == Key.Enter && it.isCtrlPressed) {
        action()
        true
    } else false
}

@Preview
@Composable
internal inline fun InputField() {
    var input by remember { mutableStateOf("") }
    val send = {
        if (input.isNotBlank()) {
            store.send(ChatIntent.SendMessage(message = input))
            input = ""
        }
    }
    TextField(
        modifier = Modifier.fillMaxWidth()
            .background(MaterialTheme.colors.background)
            .padding(pad)
            .onCtrlEnter(send),
        value = input,
        onValueChange = { input = it },
        placeholder = { Text("Message") },
        trailingIcon = {
            IconButton(
                onClick = send
            ) {
                Icon(
                    imageVector = Icons.Default.Send,
                    contentDescription = "Send",
                    tint = MaterialTheme.colors.primary
                )
            }
        }
    )
}