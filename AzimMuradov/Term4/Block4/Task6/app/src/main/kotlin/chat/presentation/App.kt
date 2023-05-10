package chat.presentation

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
import chat.data.models.MessageData
import chat.data.models.UserName
import chat.presentation.state.State
import chat.presentation.state.User
import chat.presentation.views.ChatScreen
import chat.presentation.views.LoginScreen
import chat.presentation.views.SplashScreen
import java.awt.Dimension
import java.time.Instant


fun main() {
    application {
        var state: State by remember { mutableStateOf(State.Splash) }

        Window(
            onCloseRequest = ::exitApplication,
            state = rememberWindowState(
                position = WindowPosition(alignment = Alignment.Center),
                size = DpSize(width = 500.dp, height = 700.dp),
            ),
            title = "P2P Chat",
            icon = null,
            resizable = true,
        ) {
            window.minimumSize = Dimension(500, 500)

            AppTheme(isDarkTheme = false) {
                when (val st = state) {
                    State.Splash -> {
                        SplashScreen(onSplashScreenEnd = { state = State.MainMenu })
                    }

                    State.MainMenu -> {
                        LoginScreen(
                            onLogin = {
                                state = State.ChatRoom(
                                    List(10) {
                                        listOf(
                                            User.Me to MessageData(
                                                text = "Hello!",
                                                sendTimeInEpochSecond = Instant.now().epochSecond
                                            ),
                                            User.NotMe(UserName("Bob")) to MessageData(
                                                text = "Hello!",
                                                sendTimeInEpochSecond = Instant.now().epochSecond + 10
                                            ),
                                            User.NotMe(UserName("Alice")) to MessageData(
                                                text = "Hello!",
                                                sendTimeInEpochSecond = Instant.now().epochSecond + 40
                                            ),
                                            User.Me to MessageData(
                                                text = "2 + 2?",
                                                sendTimeInEpochSecond = Instant.now().epochSecond + 102
                                            ),
                                            User.NotMe(UserName("Alice")) to MessageData(
                                                text = "2 + 2 = 4",
                                                sendTimeInEpochSecond = Instant.now().epochSecond + 500
                                            )
                                        )
                                    }.flatten()
                                )
                            }
                        )
                    }

                    is State.ChatRoom -> {
                        ChatScreen(
                            state = st,
                            onSend = { }
                        )
                    }
                }
            }
        }
    }
}
