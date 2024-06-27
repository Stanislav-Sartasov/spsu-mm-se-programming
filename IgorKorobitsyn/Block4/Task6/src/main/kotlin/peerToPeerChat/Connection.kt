package peerToPeerChat
import java.net.InetSocketAddress
import java.net.Socket
import java.nio.charset.Charset

class Connection(
    val socket: Socket
) {

    constructor(address: InetSocketAddress) : this(Socket(address.address, address.port)) {
        //this.socket.close()
    }


    fun send(data: String) {
        try {
            println("Sending: $data")

            val messageSent = data.toByteArray(Charset.forName("UTF-8"))
            socket.getOutputStream().write(messageSent)

        } catch (e: Exception) {
            println(e.message)
            println("In Connection.send")
        }
    }

    fun receive(): String {
        val messageReceived = ByteArray(1024)
        val byteRecv = socket.getInputStream().read(messageReceived)
        if (byteRecv == -1) throw Exception("Remote socket closed") //return ""
        //if (byteRecv == 0) throw Exception("string is empty")
        val res = String(messageReceived, 0, byteRecv, Charset.forName("UTF-8"))


        println("Received $res")

        return res
    }

    fun localAddress(): InetSocketAddress {
        return socket.getLocalSocketAddress() as InetSocketAddress
    }

    fun remoteAddress(): InetSocketAddress {
        return socket.getRemoteSocketAddress() as InetSocketAddress
    }

    fun close() {
        socket.close()
    }
}
