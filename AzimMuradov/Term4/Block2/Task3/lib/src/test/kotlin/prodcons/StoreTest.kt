package prodcons

import org.junit.jupiter.api.RepeatedTest
import java.util.concurrent.*
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import kotlin.test.assertFalse
import kotlin.test.assertTrue


class StoreTest {

    @RepeatedTest(5)
    fun `test empty store`() {
        val store = Store<Int> { }
        assertTrue(store.isRunning)

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test 1 to 1 store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = Store {
            +SequenceProducer(testSequence())
            +Consumer(messages::add)
        }
        assertTrue(store.isRunning)

        Thread.sleep(1000)

        store.stop()
        assertFalse(store.isRunning)

        assertContentStartEquals(testSequence(), messages.asSequence())
    }

    @RepeatedTest(5)
    fun `test 1 to many store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = Store {
            +SequenceProducer(testSequence())
            repeat(times = 5) { +Consumer(messages::add) }
        }
        assertTrue(store.isRunning)

        Thread.sleep(1000)

        store.stop()
        assertFalse(store.isRunning)

        assertContentStartEquals(testSequence(), messages.sorted().asSequence())
    }

    @RepeatedTest(5)
    fun `test many to 1 store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = Store {
            repeat(times = 5) { +SequenceProducer(testSequence()) }
            +Consumer(messages::add)
        }
        assertTrue(store.isRunning)

        Thread.sleep(1000)

        store.stop()
        assertFalse(store.isRunning)

        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = messages.groupingBy { it }.eachCount() - filler
        )
    }

    @RepeatedTest(5)
    fun `test many to many store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = Store {
            repeat(times = 5) { +SequenceProducer(testSequence()) }
            repeat(times = 5) { +Consumer(messages::add) }
        }
        assertTrue(store.isRunning)

        Thread.sleep(1000)

        store.stop()
        assertFalse(store.isRunning)

        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = messages.groupingBy { it }.eachCount() - filler
        )
    }

    @RepeatedTest(5)
    fun `test instant stop`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = Store {
            repeat(times = 5) {
                +SequenceProducer(testSequence().onEach { Thread.sleep(500) })
            }
            repeat(times = 5) {
                +Consumer<Int> {
                    Thread.sleep(500)
                    messages.add(it)
                }
            }
        }
        assertTrue(store.isRunning)

        store.stop()
        assertFalse(store.isRunning)

        assertContentEquals(expected = emptyList(), actual = messages)
    }


    companion object {

        private class SequenceProducer(messages: Sequence<Int>) : Producer<Int> {
            var iterator = messages.iterator()
            override fun produce() = iterator.next()
        }


        private val payload = listOf(4, 8, 15, 16, 23, 42)

        private const val filler = 108

        private fun testSequence() = sequence {
            yieldAll(payload)
            while (true) yield(filler)
        }

        private fun <T> assertContentStartEquals(a: Sequence<T>, b: Sequence<T>) {
            val (aSameLen, bSameLen) = (a zip b).unzip()
            assertContentEquals(aSameLen, bSameLen)
        }
    }
}
