package channels

import org.junit.jupiter.api.Test
import kotlin.test.assertContentEquals
import kotlin.test.assertTrue


class StoreConsumerTest {

    @Test
    fun `consume numbers`() {
        val store = object : Store<Int> {
            var cnt = 1

            override val isRunning: Boolean = true

            override fun receive(): Int? = if (cnt != 6) cnt++ else null

            override fun send(element: Int) = error("")
            override fun stop() = error("")
        }

        val messages = mutableListOf<Int>()

        StoreConsumer(store) { it.forEach(messages::add) }.consume()

        assertContentEquals(expected = 1..5, actual = messages)
    }

    @Test
    fun `consume cannot be started`() {
        val store = object : Store<Int> {
            override val isRunning get() = false

            override fun receive() = null

            override fun send(element: Int) = error("")
            override fun stop() = error("")
        }

        val messages = mutableListOf<Int>()

        StoreConsumer(store) { it.forEach(messages::add) }.consume()

        assertTrue(messages.isEmpty())
    }
}
