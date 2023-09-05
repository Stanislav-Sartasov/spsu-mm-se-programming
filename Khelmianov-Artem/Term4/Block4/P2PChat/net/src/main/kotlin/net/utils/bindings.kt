package net.utils

import io.ktor.network.sockets.*
import io.ktor.util.network.*
import io.ktor.utils.io.*
import java.net.DatagramSocket
import java.net.InetAddress

internal fun Socket.connection_(): Connection = Connection(this, openReadChannel(), openWriteChannel(autoFlush = true))

internal val ServerSocket.port: Int get() = localAddress.toJavaAddress().port

internal suspend fun ByteWriteChannel.writeln(str: String) = this.writeStringUtf8(str + "\n")

val loopbackAddress: String = InetAddress.getLoopbackAddress().hostAddress
val localAddress: String = DatagramSocket().use { datagramSocket ->
    datagramSocket.connect(InetAddress.getByName("8.8.8.8"), 53)
    datagramSocket.localAddress.hostAddress
}
