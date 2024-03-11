package peerToPeerChat
import java.net.InetSocketAddress
import java.util.*
import kotlin.concurrent.thread
import java.net.InetAddress

class Chat(private val port: Int) : AutoCloseable {

    private val listener: Listener = Listener(port)
    private val connections: MutableMap<InetSocketAddress, Connection?> = HashMap()
    private val sockets: MutableList<Connection> = ArrayList()
    private val stoppedLock = Any()
    @Volatile private var stopped = false
    private val receiveLock = Any()
    private val receivers: MutableList<Thread> = ArrayList()
    private val listenerThread: Thread

    var onMessage: ((String) -> Unit)? = null

    init {
        listenerThread = thread { acceptConnections() }
    }

    private fun addReceiver(conn: Connection?) {
        if (conn == null) return

        sockets.add(conn)

        val receiverThread = thread { receiveFrom(conn) }
        receivers.add(receiverThread)
    }

    private fun acceptConnections() {
        println("Started accepting loop")
        while (!stopped) {
            try {
                val connection = listener.accept() ?: continue

                synchronized(receiveLock) {
                    println("Accepted connection")

                    val needsHandshake = connection.receive()
                    connection.send("EMPTY")

                    if (needsHandshake == "N"){
                        addReceiver(connection)
                    } else {
                        val port = connection.receive().toInt()
                        connection.send("EMPTY")

                        val address = InetSocketAddress(connection.remoteAddress().address, port)

                        if (!connections.containsKey(address)) {
                            connections[address] = connection
                        }

                        val connectionConnections = connection.receive()
                        connection.send("EMPTY")

                        mergeConnections(createEndpoints(connectionConnections.split(" ")))

                        sendConnections()

                        addReceiver(connection)
                    }
                }
            } catch (e: Exception) {
                println(e.message)
                println("In Chat.acceptConnections")
            }
        }
    }

    private fun createEndpoints(strings: List<String>): List<InetSocketAddress> {
        val endpoints = mutableListOf<InetSocketAddress>()

        for (endpointString in strings) {
            try {
                val result = InetSocketAddress(endpointString.split(":")[0].removePrefix("/").removePrefix("\\"), endpointString.split(":")[1].toInt())
                endpoints.add(result)
            } catch (e: Exception) {
                println(e.message)
                println("In Chat.createEndpoints")
            }
        }

        return endpoints
    }

    private fun mergeConnections(endpoints: List<InetSocketAddress>) {
        for (endpoint in endpoints) {
            if (checkIfNeedsToBeAdded(endpoint)) {
                val connection = Connection(endpoint)
                connection.send("N")
                addReceiver(connection)
                connections[endpoint] = connection
                continue
            }

            if (!connections.containsKey(endpoint)) {
                connections[endpoint] = null
            }
        }
    }

    private fun checkIfNeedsToBeAdded(endPoint: InetSocketAddress): Boolean {
        if (endPoint.address.isLoopbackAddress && endPoint.port == port) {
            return false
        }

        return connections[endPoint] == null
    }

    private fun sendConnections() {
        val message = connections.keys.joinToString(" ") { it.toString() }

        connections.values.forEach { it?.send("LISTENERS $message") }
    }

    private fun receiveFrom(conn: Connection) {
        while (!stopped) {
            try {
                val data = conn.receive()

                if (data.isEmpty()) continue

                synchronized(receiveLock) {
                    onReceive(data)
                }
            } catch (e: Exception) {
                println(e.message)
                println("In Chat.receiveFrom")
                break
            }
        }
    }

    private fun onReceive(message: String) {
        val parts = message.split(" ")
        val t = parts[0]
        val m = parts.drop(1).joinToString(" ")

        when (t) {
            "LISTENERS" -> {
                val endpoints = createEndpoints(m.split(" "))
                mergeConnections(endpoints)
            }
            "MESSAGE" -> {
                println(m)
                onMessage?.invoke(m)
            }
        }
    }

    fun connectTo(address: InetSocketAddress) {
        println("Joining $address")

        if (connections.containsKey(address)) {
            println("Already joined")
            return
        }

        if (address.port == port) {
            println("Cannot join myself")
            return
        }

        val conn = Connection(address)

        println("Joined")

        conn.send("Y")
        conn.receive()

        conn.send(port.toString())
        conn.receive()

        connections[address] = conn

        val message = connections.keys.joinToString(" ") { it.toString() }
        conn.send(message)
        conn.receive()

        addReceiver(conn)
    }

    fun send(message: String) {
        connections.keys.forEach {
            connections[it]?.send("MESSAGE $message")
        }

        onMessage?.invoke(message)
    }

    override fun close() {
        synchronized(stoppedLock) {
            stopped = true
        }

        listener.close()

        for (connection in sockets) {
            connection.close()
        }

        listenerThread.join()
        receivers.forEach {
            if (it.isAlive) {
                it.join()
            }
        }

        println("Disposed")
    }
}
