package ui

import androidx.compose.foundation.layout.*
import androidx.compose.material.Button
import androidx.compose.material.Text
import androidx.compose.material.TextField
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import ui.mvi.ChatIntent
import ui.mvi.ChatState


@Composable
internal inline fun Connection() {
    when (val state = State.current) {
        is ChatState.Connected -> {
            var ip by remember { mutableStateOf(DEFAULT_IP) }
            var port by remember { mutableStateOf(state.port) }

            Column(Modifier.padding(pad)) {
                Text(
                    "Currently listening on port ${state.port}",
                    modifier = Modifier.align(Alignment.CenterHorizontally)
                )
                Spacer(Modifier.size(pad))
                Text(
                    "Connection options:",
                    modifier = Modifier.align(Alignment.CenterHorizontally)
                )
                TextField(
                    singleLine = true,
                    value = ip,
                    onValueChange = { ip = it },
                    label = { Text("Ip address") },
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
                    onClick = { store.send(ChatIntent.Connect(ip, port)) },
                    content = { Text("Connect") },
                    modifier = Modifier.padding(pad).fillMaxWidth()
                )
            }
        }

        else -> error("Should be unreachable")
    }
}
