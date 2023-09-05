package ui

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material.Button
import androidx.compose.material.Text
import androidx.compose.material.TextField
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import ui.mvi.ChatIntent

@Composable
internal inline fun Login() {
    var username by remember { mutableStateOf(DEFAULT_USERNAME) }
    var port by remember { mutableStateOf(DEFAULT_PORT) }

    Column(Modifier.fillMaxSize().padding(pad).thinBorder()) {
        TextField(
            singleLine = true,
            value = username,
            onValueChange = { username = it },
            label = { Text("Username") },
            modifier = Modifier.padding(pad).fillMaxWidth()
        )
        TextField(
            singleLine = true,
            value = port.toString(),
            onValueChange = { port = it.toIntOrNull()?.coerceIn(0..65535) ?: port },
            label = { Text("Port") },
            modifier = Modifier.padding(pad).fillMaxWidth()
        )
        Button(
            onClick = { store.send(ChatIntent.Start(port, username)) },
            content = { Text("Start") },
            modifier = Modifier.padding(pad).fillMaxWidth()
        )
    }
}
