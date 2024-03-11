package peerToPeerChat
import java.net.InetAddress
import java.net.InetSocketAddress
import java.net.ServerSocket

class Listener(port: Int) {
    private val serverSocket: ServerSocket = ServerSocket()
    private val bound: InetSocketAddress

    init {
        val localEndPoint = InetSocketAddress(InetAddress.getByName(null), port)

        serverSocket.bind(localEndPoint)
        bound = serverSocket.localSocketAddress as InetSocketAddress
    }

    fun accept(): Connection? {
        return try {
            val clientSocket = serverSocket.accept()
            return Connection(clientSocket)
        } catch (e: Exception) {
            println(e.message)
            println("In Listener.accept")
            null
        }
    }

    fun close() {
        serverSocket.close()
    }
}
