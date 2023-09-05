package net

import domain.ChatServerInterface
import domain.Message
import domain.User
import kotlinx.coroutines.async
import kotlinx.coroutines.awaitAll
import kotlinx.coroutines.flow.filter
import kotlinx.coroutines.flow.firstOrNull
import kotlinx.coroutines.flow.toList
import kotlinx.coroutines.flow.transform
import kotlinx.coroutines.runBlocking
import net.utils.loopbackAddress
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.Assertions.assertAll
import org.junit.jupiter.api.BeforeAll
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.function.Executable
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import java.net.Socket
import kotlin.test.assertEquals
import kotlin.test.assertNotNull


class ServerTest {
    private lateinit var sl: List<ChatServerInterface>

    @AfterEach
    fun tearDown() {
        sl.forEach { it.close() }
    }

    @Test
    fun start() {
        sl = createServers()
        val s = sl.first()
        assertAll(
            { assert(s.user.name == "0") },
            { assert(isListening(PORT)) }
        )
        s.close()
        Thread.sleep(100)
        assert(!isListening(PORT))
    }

    @ParameterizedTest
    @ValueSource(ints = [3, 4, 5, 6, 7])
    fun formCompleteGraph(n: Int) = runBlocking {
        sl = createServers(n)
        val res = sl.slice(0 until sl.size - 1)
            .mapIndexed { i, s -> async { s.connect(loopbackAddress, PORT + (i + 1) % n) } }.awaitAll()

        assert(res.all { it.isSuccess })
        Thread.sleep(1000)

        assertAll(sl.stream().map { Executable { assert(it.connectedUsers.value.size == (n - 1)) } })
        Thread.sleep(100)
    }

    @Test
    fun disconnectIntermediates(): Unit = runBlocking {
        val n = 5
        sl = createServers(n)
        val res = sl.slice(0 until sl.size - 1)
            .mapIndexed { i, s -> async { s.connect(loopbackAddress, PORT + i + 1) } }
            .awaitAll()
        assert(res.all { it.isSuccess })

        Thread.sleep(500)
        sl.slice(1 until n - 1).forEach { it.close() }

        Thread.sleep(500)
        sl[0].apply { broadcastMessage(Message.Normal(from = user.name, msg = "test")) }
        Thread.sleep(500)

        assert(sl[0].connectedUsers.value.size == 1)
        assert(sl[0].connectedUsers.value[0].name == (n - 1).toString())
        assert(sl[n - 1].connectedUsers.value.size == 1)
        assert(sl[n - 1].connectedUsers.value[0].name == "0")

        sl.forEach{it.close()}
        val m = sl[n - 1].receivedMessages.filter { msg -> msg is Message.Normal }.firstOrNull()
        assertNotNull(m)
    }

    @Test
    fun broadcast(): Unit = runBlocking {
        /*      0         3
        *     /  \        |
        *   /     \       |
        *  2 - - - 1      4
        *
        * 0 sends msg1
        * 3 sends msg2
        * 2 connects to 4
        * 1 sends msg3
        * */
        sl = createServers(5)
        val res1 = sl.slice(0 until 2)
            .mapIndexed { i, s -> async { s.connect(loopbackAddress, PORT + i + 1) } }
            .awaitAll()
        val res2 = sl.slice(3 until 4)
            .mapIndexed { i, s -> async { s.connect(loopbackAddress, PORT + i + 4) } }
            .awaitAll()
        assert(res1.all { it.isSuccess })
        assert(res2.all { it.isSuccess })

        val msg1 = Message.Normal(from = sl[0].user.name, msg = "first")
        val msg2 = Message.Normal(from = sl[3].user.name, msg = "second")
        val msg3 = Message.Normal(from = sl[1].user.name, msg = "third")

        Thread.sleep(500)
        sl[0].broadcastMessage(msg1)
        sl[3].broadcastMessage(msg2)
        Thread.sleep(500)

        assert(sl[0].connectedUsers.value.size == 2)
        assert(sl[1].connectedUsers.value.size == 2)
        assert(sl[2].connectedUsers.value.size == 2)
        assert(sl[3].connectedUsers.value.size == 1)
        assert(sl[4].connectedUsers.value.size == 1)

        sl[2].connect(loopbackAddress, PORT + 4)
        Thread.sleep(500)
        sl[1].broadcastMessage(msg3)
        Thread.sleep(500)

        sl.forEach { it.close() }
        val received = List(sl.size) { mutableListOf<Message.Normal>() }
        sl.forEachIndexed { i, it ->
            it.receivedMessages.filter { it is Message.Normal }
                .transform<Message, Message.Normal> { v -> if (v is Message.Normal) emit(v) }
                .toList(received[i])
        }

        assertEquals(
            expected = List(5) {
                if (it < 3) listOf(msg1, msg3)
                else listOf(msg2, msg3)
            },
            actual = received.map { it.toList() });
    }

    companion object {
        const val PORT = 10000
        fun createServers(n: Int = 1): List<ChatServerInterface> = buildList {
            repeat(n) {
                add(Server().apply {
                    start(user = User(name = it.toString()), port = PORT + it)
                })
            }
        }

        fun isListening(port: Int): Boolean = try {
            Socket(loopbackAddress, port).use { true }
        } catch (e: Throwable) {
            false
        }

        @JvmStatic
        @BeforeAll
        fun beforeAll() {
            Server.PING_INTERVAL = 250L
            Server.CONNECTION_DELAY = 250L
        }
    }
}