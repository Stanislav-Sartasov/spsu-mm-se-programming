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


@Composable
fun LoginScreen(onLogin: (String, String, Int) -> Unit) {
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
                value = hubAddress/* run {
                    val ip = mutableListOf("0", "0", "0", "0")
                    hubAddress.chunked(3).forEachIndexed { i, x -> ip[i] = x }
                    ip.joinToString(separator = ".")
                } */,
                onValueChange = {
                    // val ip = it.filterNot { it == '.' }.chunked(3).map { it.toIntOrNull() }
                    // if (it.all { it.isDigit() || it == '.' } && ip.all { it != null && it in 0..255 } && ip.size <= 4) {
                    //     hubAddress =
                    //         it.filterNot { it == '.' }.chunked(3).map { it.toInt().toString() }.joinToString("")
                    // }
                    hubAddress = it
                },
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
                onValueChange = { hubPort = it },
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
                .then(if (username.trim().length in 3..16) Modifier.pointerHoverIcon(PointerIcon.Hand) else Modifier),
            enabled = username.trim().length in 3..16
        ) {
            Text(text = "JOIN")
        }

        Spacer(Modifier.weight(1f))
    }
}
