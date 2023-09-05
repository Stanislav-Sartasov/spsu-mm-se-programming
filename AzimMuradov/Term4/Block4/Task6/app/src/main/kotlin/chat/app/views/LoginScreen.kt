package chat.app.views

import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.pointer.PointerIcon
import androidx.compose.ui.input.pointer.pointerHoverIcon
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import chat.app.state.State


@Composable
fun LoginScreen(state: State.LoginScreen, onLogin: (username: String, hubAddress: String, hubPort: Int) -> Unit) {
    Column(
        modifier = Modifier.fillMaxSize().padding(100.dp),
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Spacer(Modifier.weight(1f))

        var username by remember { mutableStateOf("") }
        var hubAddress by remember { mutableStateOf("") }
        var hubPort by remember { mutableStateOf("") }

        OutlinedTextField(
            value = username,
            onValueChange = { if (it.length <= 16) username = it },
            modifier = Modifier.widthIn(max = 400.dp).fillMaxWidth().height(64.dp),
            textStyle = LocalTextStyle.current.copy(textAlign = TextAlign.Center),
            label = { Text(text = "username") },
            singleLine = true,
            colors = TextFieldDefaults.outlinedTextFieldColors(
                backgroundColor = MaterialTheme.colors.surface
            )
        )

        Spacer(Modifier.size(8.dp))

        Row(Modifier.widthIn(max = 400.dp).fillMaxWidth().height(64.dp)) {
            OutlinedTextField(
                value = hubAddress,
                onValueChange = { if (it.length <= 15) hubAddress = it },
                modifier = Modifier.weight(1f).fillMaxHeight(),
                textStyle = LocalTextStyle.current.copy(textAlign = TextAlign.Center),
                label = { Text(text = "hub IP") },
                singleLine = true,
                colors = TextFieldDefaults.outlinedTextFieldColors(
                    backgroundColor = MaterialTheme.colors.surface
                )
            )

            Spacer(Modifier.size(8.dp))

            OutlinedTextField(
                value = hubPort,
                onValueChange = { if (it.toIntOrNull() != null || it.isEmpty()) hubPort = it },
                modifier = Modifier.weight(1f).fillMaxHeight(),
                textStyle = LocalTextStyle.current.copy(textAlign = TextAlign.Center),
                label = { Text(text = "hub port") },
                singleLine = true,
                colors = TextFieldDefaults.outlinedTextFieldColors(
                    backgroundColor = MaterialTheme.colors.surface
                )
            )
        }

        Spacer(Modifier.size(16.dp))

        Button(
            onClick = { onLogin(username.trim(), hubAddress, hubPort.toInt()) },
            modifier = Modifier
                .widthIn(max = 400.dp)
                .fillMaxWidth()
                .height(64.dp)
                .then(
                    if (username.trim().length in 3..16 && hubAddress.isNotEmpty() && hubPort.isNotEmpty()) {
                        Modifier.pointerHoverIcon(PointerIcon.Hand)
                    } else {
                        Modifier
                    }
                ),
            enabled = username.trim().length in 3..16 && hubAddress.isNotEmpty() && hubPort.isNotEmpty()
        ) {
            Text(text = "JOIN")
        }

        if (state is State.LoginScreen.Error) {
            Spacer(Modifier.size(32.dp))
            Text(
                text = "${state.message}, please try again",
                modifier = Modifier
                    .widthIn(max = 400.dp)
                    .fillMaxWidth(),
                color = MaterialTheme.colors.error, textAlign = TextAlign.Center
            )
        }

        Spacer(Modifier.weight(1f))
    }
}
