package chat.app

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.unit.DpSize
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.WindowPosition
import androidx.compose.ui.window.application
import androidx.compose.ui.window.rememberWindowState
import chat.app.state.Message
import chat.app.state.State
import chat.app.state.User
import chat.app.views.ChatScreen
import chat.app.views.LoginScreen
import chat.app.views.SplashScreen
import chat.peer.Intent
import chat.peer.Peer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.awt.Dimension
import java.time.Instant
import kotlin.random.Random
import kotlin.random.nextInt

fun main() {
    val scope = CoroutineScope(Dispatchers.IO)

    val peer = Peer().apply {
        run(
            hubIp = "127.0.0.1",
            hubPort = 12345,
            peerServerPort = Random.nextInt(range = 4444..7777)
        )
    }

    application {
        var state: State by remember { mutableStateOf(State.SplashScreen) }

        scope.launch {
            peer.state.collect { st ->
                if (st is chat.peer.State.Chat) {
                    state = State.ChatScreen(
                        st.messages.map { (user, message) ->
                            Message(
                                sender = if (user.username == peer.username) User.Me else User.NotMe(user.username),
                                text = message.text,
                                sendTime = message.sendTime
                            )
                        }.sortedBy(Message::sendTime)
                    )
                }
            }
        }

        Window(
            onCloseRequest = ::exitApplication,
            state = rememberWindowState(
                position = WindowPosition(alignment = Alignment.Center),
                size = DpSize(width = 500.dp, height = 700.dp),
            ),
            title = "P2P Chat" + if (state is State.ChatScreen) " - ${peer.username}" else "",
            icon = null,
            resizable = true,
        ) {
            window.minimumSize = Dimension(500, 500)

            AppTheme(isDarkTheme = false) {
                when (val st = state) {
                    State.SplashScreen -> {
                        SplashScreen(onSplashScreenEnd = { state = State.LoginScreen })
                    }

                    State.LoginScreen -> {
                        LoginScreen(
                            onLogin = {
                                scope.launch {
                                    peer.intents.emit(Intent.JoinTheChat(username = it))
                                }
                            }
                        )
                    }

                    is State.ChatScreen -> {
                        ChatScreen(
                            state = st,
                            onSend = {
                                scope.launch {
                                    peer.intents.emit(Intent.SendMessage(text = it, sendTime = Instant.now()))
                                }
                            }
                        )
                    }
                }
            }
        }
    }
}
