package chat.app

import androidx.compose.foundation.layout.*
import androidx.compose.material.Button
import androidx.compose.material.MaterialTheme
import androidx.compose.material.OutlinedButton
import androidx.compose.material.Text
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.DpSize
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.*
import chat.app.state.Message
import chat.app.state.State
import chat.app.state.User
import chat.app.views.ChatScreen
import chat.app.views.LoginScreen
import chat.app.views.SplashScreen
import chat.logger
import chat.peer.Intent
import chat.peer.LoginException
import chat.peer.Peer
import kotlinx.coroutines.*
import java.awt.Dimension
import java.io.IOException
import java.time.Instant
import kotlin.time.Duration.Companion.milliseconds
import chat.peer.State as PeerState


fun main() {
    val scope = CoroutineScope(Dispatchers.Default)

    Peer().use { peer ->
        peer.run()

        application {
            var state: State by remember { mutableStateOf(State.SplashScreen) }

            scope.launch {
                peer.state.collect { st ->
                    if (st is PeerState.Chat) {
                        state = if (st.users.size == 1) {
                            State.ChatScreen.Alone
                        } else {
                            State.ChatScreen.NotAlone(
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
            }

            var isOpen by remember { mutableStateOf(true) }
            var isAskingToClose by remember { mutableStateOf(false) }

            if (isOpen) {
                Window(
                    onCloseRequest = { isAskingToClose = true },
                    state = rememberWindowState(
                        position = WindowPosition(alignment = Alignment.Center),
                        size = DpSize(width = 500.dp, height = 700.dp),
                    ),
                    title = "P2P Chat" + if (state is State.ChatScreen) " - ${peer.username}" else "",
                    icon = null,
                    resizable = true,
                ) {
                    window.minimumSize = Dimension(500, 500)

                    if (isAskingToClose) {
                        Dialog(
                            onCloseRequest = { isAskingToClose = false },
                            title = "Confirm Exit",
                        ) {
                            Box(Modifier.fillMaxSize(), Alignment.Center) {
                                Column(
                                    verticalArrangement = Arrangement.spacedBy(16.dp),
                                    horizontalAlignment = Alignment.CenterHorizontally
                                ) {
                                    Text(
                                        text = "Are you sure you want to exit?",
                                        style = MaterialTheme.typography.subtitle1
                                    )
                                    Row(horizontalArrangement = Arrangement.spacedBy(8.dp)) {
                                        OutlinedButton(onClick = { isOpen = false }) { Text(text = "EXIT") }
                                        Button(onClick = { isAskingToClose = false }) { Text(text = "CANCEL") }
                                    }
                                }
                            }
                        }
                    }

                    AppTheme {
                        when (val st = state) {
                            State.SplashScreen -> SplashScreen(onSplashScreenEnd = { state = State.LoginScreen.Idle })

                            is State.LoginScreen -> LoginScreen(
                                state = st,
                                onLogin = { username, hubAddress, hubPort ->
                                    scope.launch {
                                        try {
                                            peer.connectToHub(hubAddress, hubPort)
                                        } catch (e: IOException) {
                                            logger.warn(e.stackTraceToString())
                                            state = State.LoginScreen.Error("failed to connect to the server")
                                        } catch (e: LoginException) {
                                            logger.warn(e.stackTraceToString())
                                            state = State.LoginScreen.Error(e.message)
                                        }
                                    }
                                    scope.launch {
                                        delay(100.milliseconds)
                                        peer.intents.emit(Intent.JoinTheChat(username))
                                    }
                                }
                            )

                            is State.ChatScreen -> ChatScreen(
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

    scope.cancel()
}
