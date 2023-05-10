package chat.hub

import chat.data.UserData
import chat.data.Username
import java.io.PrintWriter


data class State(
    val users: Map<Username, Pair<UserData, PrintWriter>>,
)
