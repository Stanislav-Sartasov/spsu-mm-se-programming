package net

import domain.ChatServerInterface
import domain.Message
import domain.User
import io.ktor.network.selector.*
import io.ktor.network.sockets.*
import io.ktor.utils.io.*
import kotlinx.coroutines.*
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.consumeAsFlow
import kotlinx.serialization.decodeFromString
import kotlinx.serialization.encodeToString
import kotlinx.serialization.json.Json
import net.utils.*
import java.io.IOException
import java.net.BindException
import java.net.SocketException
import java.nio.channels.ClosedChannelException
import java.util.concurrent.ConcurrentSkipListSet
import kotlin.random.Random
import kotlin.random.nextLong

class Server : ChatServerInterface {
    override var user: User = User("_")
        private set

    private val io = Dispatchers.IO
    private val scope: CoroutineScope = CoroutineScope(io)
    private val selectorManager = ActorSelectorManager(io)
    private lateinit var serverSocket: ServerSocket

    private val seenMessageIds = ConcurrentSkipListSet<Int>()

    private val connections = SimpleHashMap<User, Connection>()
    override var connectedUsers: StateFlow<List<User>> = connections.flow

    private val incomingMessages = Channel<Message>(Channel.UNLIMITED)
    private val outgoingMessages = Channel<Message>(Channel.UNLIMITED)
    override val receivedMessages = incomingMessages.consumeAsFlow()


    override fun start(user: User, port: Int): Result<Int> {
        this.user = user
        try {
            serverSocket = aSocket(selectorManager)
                .tcp()
                .bind("0.0.0.0"/*listen on all interfaces*/, port)
        } catch (e: BindException) {
            logger.warning("Port $port in use")
            return Result.failure(e)
        }

        listen()
        logger.info("Start listening on port $port")
        send()
        ping()
        return Result.success(port)
    }

    private fun listen() = scope.launch {
        try {
            while (isActive) connectionHandler(serverSocket.accept())
        } catch (_: ClosedChannelException) {
        }
    }

    private fun send() = scope.launch {
        for (message in outgoingMessages) {
            Json.encodeToString(message).let { msg ->
                connections.values.forEach { connection ->
                    try {
                        connection.output.writeln(msg)
                    } catch (e: IOException) {
                        logger.info("${connection.socket} $e")
                    }
                }
            }
        }
    }

    private fun ping() = scope.launch {
        while (isActive) {
            _broadcastMessage(
                Message.Service.Ping(from = user, ip = localAddress, port = serverSocket.port),
                sendToIncoming = false
            )
            delay(PING_INTERVAL)
        }
    }

    override suspend fun connect(ip: String, port: Int): Result<Unit> {
        return try {
            aSocket(selectorManager).tcp().connect(ip, port).let {
                connectionHandler(it)
            }
            Result.success(Unit)
        } catch (e: SocketException) {
            logger.info(e.toString())
            Result.failure(e)
        }
    }

    override suspend fun broadcastMessage(message: Message) = _broadcastMessage(message, true)
    private suspend fun _broadcastMessage(message: Message, sendToIncoming: Boolean) {
        seenMessageIds.add(message.id)
        if (sendToIncoming) incomingMessages.send(message)
        outgoingMessages.send(message)
    }

    private fun connectionHandler(socket: Socket) = scope.launch {
        socket.use {
            val conn = socket.connection_()
            var from = User()

            try {
                from = handshake(conn) ?: return@launch

                logger.info("Connect to ${conn.socket.remoteAddress}")
                while (isActive && !conn.input.isClosedForRead) {
                    conn.input.readUTF8Line()
                        ?.run<String, Message>(Json::decodeFromString)
                        ?.takeIf { seenMessageIds.add(it.id) }
                        ?.run { processMessage(this) }
                }
            } catch (e: Throwable) {
                logger.warning(e.toString())
            } finally {
                logger.info("Disconnected from ${conn.socket}")
                connections.remove(from)
                conn.output.close()
                socket.close()
            }
        }
    }

    private suspend fun handshake(conn: Connection): User? {
        conn.output.writeln(Json.encodeToString(Message.Service.Ping(user, localAddress, serverSocket.port)))
        return withTimeoutOrNull(1000) {
            conn.input.readUTF8Line()
                ?.run { Json.decodeFromString<Message.Service.Ping>(this) }
                ?.takeIf {
                    seenMessageIds.add(it.id)
                            && it.from != user
                            && connections.put(it.from, conn) == null
                }
        }?.from
    }

    private suspend fun processMessage(message: Message) = scope.launch {
        when (message) {
            is Message.Normal -> {
                _broadcastMessage(message, sendToIncoming = true)
            }

            is Message.Service.Ping -> {
                _broadcastMessage(message, sendToIncoming = false)
                delay(Random.nextLong(0..CONNECTION_DELAY))
                if (message.from !in connectedUsers.value) {
                    connect(message.ip, message.port)
                }
            }
        }
    }

    override fun close() {
        if (!scope.isActive) return
        scope.cancel()
        if (::serverSocket.isInitialized) serverSocket.close()
        connections.forEach { (_, c) ->
            c.output.close()
            c.socket.close()
        }
        incomingMessages.close()
        outgoingMessages.close()
        selectorManager.close()
        logger.warning("Stopped server")
    }

    companion object {
        var PING_INTERVAL = 5000L
        var CONNECTION_DELAY = 500L
    }
}