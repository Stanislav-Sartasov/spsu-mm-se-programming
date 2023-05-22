package chat.hub.state

import chat.data.UserData
import java.io.PrintWriter
import java.net.SocketAddress


data class Connection(
    val user: UserData,
    val address: SocketAddress,
    val printer: PrintWriter,
)
