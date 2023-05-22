package chat.hub

import app.cash.turbine.test
import chat.data.UserData
import chat.hub.state.State
import chat.reader
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.runBlocking
import org.junit.jupiter.api.assertDoesNotThrow
import java.net.InetSocketAddress
import java.net.Socket
import kotlin.concurrent.thread
import kotlin.test.Test
import kotlin.test.assertEquals


class HubTest {

    @Test
    fun `empty run`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                }
            }

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and attempt to connect`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use { Thread.sleep(500) }
                }
                thread.join()
            }

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and try to login`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        assertEquals(
                            expected = """{"type":"chat.data.H2PNews.ChatInfo","users":[{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}]}""",
                            actual = it.reader().buffered().readLine()
                        )

                        assertEquals(
                            expected = listOf(
                                UserData("John Doe", InetSocketAddress("127.0.0.1", 5000))
                            ),
                            actual = awaitItem().connections.map { it.user }
                        )

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and try to send not valid 'news'`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write("command\n".encodeToByteArray())

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and try to login twice`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        awaitItem()

                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and fail to login with taken username`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        awaitItem()

                        Socket("localhost", 1234).use {
                            it.getOutputStream().write(
                                """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                                   |
                                """.trimMargin().encodeToByteArray()
                            )

                            assertEquals(
                                expected = """{"type":"chat.data.H2PNews.LoginError","message":"username is taken"}""",
                                actual = it.reader().buffered().readLine()
                            )
                        }

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and fail to login with taken address`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        awaitItem()

                        Socket("localhost", 1234).use {
                            it.getOutputStream().write(
                                """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Carpenter","address":{"ip":"127.0.0.1","port":5000}}}
                                   |
                                """.trimMargin().encodeToByteArray()
                            )

                            assertEquals(
                                expected = """{"type":"chat.data.H2PNews.LoginError","message":"address is taken"}""",
                                actual = it.reader().buffered().readLine()
                            )
                        }

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }

    @Test
    fun `run and fail to change user data`() = runBlocking {
        val state = MutableStateFlow(State(emptyList()))

        var thread: Thread

        state.test {
            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertDoesNotThrow {
                Hub(state = state).use {
                    thread = thread { it.run(port = 1234) }.also { Thread.sleep(500) }
                    Socket("localhost", 1234).use {
                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Doe","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        it.reader().buffered().readLine()

                        awaitItem()

                        it.getOutputStream().write(
                            """|{"type":"chat.data.P2HNews.JoinTheChatRequest","user":{"username":"John Malkovich","address":{"ip":"127.0.0.1","port":5000}}}
                               |
                            """.trimMargin().encodeToByteArray()
                        )

                        assertEquals(
                            expected = """{"type":"chat.data.H2PNews.LoginError","message":"cannot change user data"}""",
                            actual = it.reader().buffered().readLine()
                        )

                        Thread.sleep(500)
                    }
                }
                thread.join()
            }

            assertEquals(expected = State(emptyList()), actual = awaitItem())

            assertEquals(emptyList(), cancelAndConsumeRemainingEvents())
        }
    }
}
