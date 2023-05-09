package chat

import chat.data.models.UserData
import chat.data.models.UserName
import java.io.InputStreamReader
import java.io.PrintWriter


data class State(
    val users: Map<UserName, FullUserData>,
)

data class FullUserData(val userData: UserData, val reader: InputStreamReader, val printer: PrintWriter)
