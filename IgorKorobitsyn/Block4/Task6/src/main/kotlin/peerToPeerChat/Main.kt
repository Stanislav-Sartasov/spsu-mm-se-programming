package peerToPeerChat

import java.net.InetSocketAddress

fun main(args: Array<String>) {
    println("Enter port")
    val port = readlnOrNull()?.toIntOrNull()

    if (port != null) {
        try {
            workCycle(port)
        } catch (e: Exception) {
            println(e.message)
            println(e.stackTraceToString())
            println("In main")
            readlnOrNull()
        }
    }
}

private fun workCycle(port: Int) {
    val chat = Chat(port)

    while (true) {
        val cmd = readlnOrNull()
        val parts = cmd?.split(" ")
        val t = parts?.get(0)
        val m = parts?.drop(1)?.joinToString(" ")

        when (t) {
            "connect" -> chat.connectTo(InetSocketAddress("127.0.0.1", m?.toIntOrNull() ?: 0))
            else -> chat.send(cmd.orEmpty())
        }
    }
}