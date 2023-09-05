package ui

import androidx.compose.desktop.ui.tooling.preview.Preview
import androidx.compose.foundation.layout.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier

@Preview
@Composable
internal inline fun Chat() {
    Row {
        Column(
            Modifier.fillMaxSize().thinBorder().weight(.7f),
            verticalArrangement = Arrangement.Bottom
        ) {
            Column(modifier = Modifier.fillMaxSize()) {
                Box(Modifier.weight(1f)) {
                    Messages()
                }
                Spacer(Modifier.size(pad))
                InputField()
            }
        }
        Column(Modifier.fillMaxSize().thinBorder().weight(.3f)) {
            Box(Modifier.fillMaxSize().weight(.7f)) {
                Users()
            }
            Spacer(Modifier.size(pad))
            Connection()
        }
    }
}