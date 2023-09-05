package chat.peer

import app.cash.turbine.test
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.assertDoesNotThrow
import java.net.ServerSocket
import kotlin.test.Test
import kotlin.test.assertEquals


class PeerTest {

    private lateinit var server: ServerSocket


    @BeforeEach
    fun setUp() {
        server = ServerSocket(12345)
    }

    @AfterEach
    fun tearDown() {
        server.close()
    }


    @Test
    fun `empty run`() = runBlocking {
        val state = MutableStateFlow<State>(State.Idle)

        state.test {
            assertEquals(expected = State.Idle, actual = awaitItem())

            assertDoesNotThrow {
                Peer(_state = state).use {
                    it.run(peerServerPort = 5000).also { Thread.sleep(500) }
                }
            }

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `empty run with default 'peerServerPort'`() = runBlocking {
        val state = MutableStateFlow<State>(State.Idle)

        state.test {
            assertEquals(expected = State.Idle, actual = awaitItem())

            assertDoesNotThrow {
                Peer(_state = state).use {
                    it.run().also { Thread.sleep(500) }
                }
            }

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }
}
