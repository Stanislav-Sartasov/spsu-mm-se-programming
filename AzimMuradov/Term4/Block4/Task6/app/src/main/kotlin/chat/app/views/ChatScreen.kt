package chat.app.views

import androidx.compose.foundation.VerticalScrollbar
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.foundation.rememberScrollbarAdapter
import androidx.compose.foundation.text.selection.LocalTextSelectionColors
import androidx.compose.foundation.text.selection.SelectionContainer
import androidx.compose.foundation.text.selection.TextSelectionColors
import androidx.compose.material.*
import androidx.compose.material.TextFieldDefaults.IconOpacity
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Send
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.pointer.PointerIcon
import androidx.compose.ui.input.pointer.pointerHoverIcon
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import chat.app.state.State


@Composable
fun ChatScreen(state: State.ChatScreen, onSend: (String) -> Unit) {
    Column(
        modifier = Modifier.fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Box(Modifier.fillMaxWidth().height(16.dp).background(MaterialTheme.colors.secondary))

        Box(Modifier.fillMaxWidth().weight(1f).padding(end = 6.dp)) {
            val lazyListState = rememberLazyListState()

            when (state) {
                State.ChatScreen.Alone -> Box(
                    modifier = Modifier.fillMaxSize().padding(64.dp),
                    contentAlignment = Alignment.Center
                ) {
                    Text(
                        text = "You cannot send messages while you are alone in the chat",
                        textAlign = TextAlign.Center
                    )
                }

                is State.ChatScreen.NotAlone -> SelectionContainer {
                    LazyColumn(
                        modifier = Modifier.fillMaxSize(),
                        state = lazyListState,
                        contentPadding = PaddingValues(16.dp),
                        verticalArrangement = Arrangement.spacedBy(8.dp, Alignment.Bottom)
                    ) {
                        items(state.messages) { msg ->
                            MessageBox(msg)
                        }
                    }
                }
            }

            VerticalScrollbar(
                modifier = Modifier.padding(vertical = 16.dp).align(Alignment.CenterEnd).fillMaxHeight(),
                adapter = rememberScrollbarAdapter(scrollState = lazyListState)
            )
        }

        Row(
            Modifier.fillMaxWidth().background(MaterialTheme.colors.secondary).padding(16.dp),
            Arrangement.SpaceBetween,
            Alignment.Bottom
        ) {
            var value by remember { mutableStateOf("") }

            Column(Modifier.weight(1f), horizontalAlignment = Alignment.Start) {
                CompositionLocalProvider(
                    LocalTextSelectionColors provides TextSelectionColors(
                        handleColor = Color.White,
                        backgroundColor = Color.Blue
                    )
                ) {
                    OutlinedTextField(
                        value = value,
                        onValueChange = { if (it.length <= 300) value = it },
                        modifier = Modifier.fillMaxWidth().heightIn(min = 56.dp, max = 128.dp),
                        placeholder = { Text(text = "write a message...") },
                        colors = TextFieldDefaults.outlinedTextFieldColors(
                            textColor = MaterialTheme.colors.onSecondary,
                            backgroundColor = Color.Transparent,
                            cursorColor = MaterialTheme.colors.onSecondary,
                            focusedBorderColor = Color.Transparent,
                            unfocusedBorderColor = Color.Transparent,
                            trailingIconColor = MaterialTheme.colors.onSurface.copy(alpha = IconOpacity),
                            placeholderColor = MaterialTheme.colors.onSecondary.copy(ContentAlpha.medium),
                        )
                    )
                }
                Text(
                    text = "${value.length} / 300",
                    modifier = Modifier.padding(start = 16.dp),
                    color = if (value.length < 300) MaterialTheme.colors.onSecondary else MaterialTheme.colors.error,
                    fontWeight = if (value.length < 300) FontWeight.Normal else FontWeight.Bold,
                    fontFamily = FontFamily.Monospace,
                    style = MaterialTheme.typography.overline
                )
            }

            Spacer(Modifier.size(16.dp))

            Column(Modifier.height(72.dp), verticalArrangement = Arrangement.Center) {
                IconButton(
                    onClick = {
                        onSend(value)
                        value = ""
                    },
                    enabled = value.isNotEmpty() && state is State.ChatScreen.NotAlone
                ) {
                    Icon(
                        imageVector = Icons.Filled.Send,
                        contentDescription = "send message",
                        modifier = Modifier
                            .size(24.dp)
                            .then(
                                if (value.isNotEmpty() && state is State.ChatScreen.NotAlone) {
                                    Modifier.pointerHoverIcon(PointerIcon.Hand)
                                } else {
                                    Modifier
                                }
                            ),
                        tint = MaterialTheme.colors.onSecondary.copy(
                            alpha = if (value.isNotEmpty() && state is State.ChatScreen.NotAlone) {
                                1f
                            } else {
                                ContentAlpha.disabled
                            }
                        )
                    )
                }
            }
        }
    }
}
