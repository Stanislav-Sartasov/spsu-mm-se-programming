package ui

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material.Text
import androidx.compose.material.darkColors
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.SpanStyle
import androidx.compose.ui.text.buildAnnotatedString
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.withStyle
import ui.mvi.ChatState

@Composable
internal inline fun Users() {
    val state = State.current as ChatState.Connected
    val listState = rememberLazyListState()
    Column(Modifier.padding(pad)) {
        Text(
            buildAnnotatedString {
                append("Logged in as ")
                withStyle(SpanStyle(color = darkColors().primary)) {
                    append(state.username)
                }
            }
        )
        Spacer(Modifier.size(pad))
        Text("Connected users:", fontWeight = FontWeight.Bold)
        LazyColumn(state = listState) {
            items(state.knownUsers) { user ->
                Text(user.name)
            }
        }
    }
}