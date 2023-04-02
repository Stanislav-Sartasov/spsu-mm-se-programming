package channels

import org.junit.jupiter.api.Test
import kotlin.test.assertContentEquals
import kotlin.test.assertTrue


class StoreProducerTest {

    @Test
    fun `produce numbers`() {
        val store = object : Store<Int> {
            val messages = mutableListOf<Int>()

            override val isRunning: Boolean = true

            override fun send(element: Int): Boolean {
                messages += element
                return true
            }

            override fun receive() = error("")
            override fun stop() = error("")
        }

        StoreProducer(store, sequenceOf(1, 2, 3)).produce()

        assertContentEquals(expected = listOf(1, 2, 3), actual = store.messages)
    }

    @Test
    fun `produce cannot be started`() {
        val store = object : Store<Int> {
            val messages = mutableListOf<Int>()

            override val isRunning: Boolean = false

            override fun send(element: Int): Boolean {
                messages += element
                return true
            }

            override fun receive() = error("")
            override fun stop() = error("")
        }

        StoreProducer(store, sequenceOf(1, 2, 3)).produce()

        assertTrue(store.messages.isEmpty())
    }

    @Test
    fun `produce stopped mid-process`() {
        val store = object : Store<Int> {
            val messages = mutableListOf<Int>()

            override var isRunning: Boolean = true
                private set

            override fun send(element: Int): Boolean {
                messages += element
                isRunning = false
                return false
            }

            override fun receive() = error("")
            override fun stop() = error("")
        }

        StoreProducer(store, sequenceOf(1, 2, 3)).produce()

        assertContentEquals(expected = listOf(1), actual = store.messages)
    }
}
