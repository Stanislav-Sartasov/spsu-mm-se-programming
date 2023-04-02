package channels

import org.junit.jupiter.api.RepeatedTest
import java.util.concurrent.*
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import kotlin.test.assertFalse
import kotlin.test.assertTrue


class MonitoredStoreTest {

    @RepeatedTest(5)
    fun `test empty store`() {
        val store = MonitoredStore<Int> { }
        assertTrue(store.isRunning)

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test 1 to 1 store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            store += MockProducer(store, listOf(4, 8, 15, 16, 23, 42))
            store += MockConsumer(store, messages::add)
        }
        assertTrue(store.isRunning)

        Thread.sleep(500)
        assertContentEquals(
            expected = listOf(4, 8, 15, 16, 23, 42),
            actual = messages
        )

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test 1 to many store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            store += MockProducer(store, listOf(4, 8, 15, 16, 23, 42))
            repeat(times = 5) {
                store += MockConsumer(store) { assertTrue(messages.add(it)) }
            }
        }
        assertTrue(store.isRunning)

        Thread.sleep(500)
        assertEquals(messages.size, messages.distinct().size)
        assertEquals(
            expected = setOf(4, 8, 15, 16, 23, 42),
            actual = messages.toSet()
        )

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test many to 1 store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            repeat(times = 5) {
                store += MockProducer(store, listOf(4, 8, 15, 16, 23, 42))
            }
            store += MockConsumer(store, messages::add)
        }
        assertTrue(store.isRunning)

        Thread.sleep(500)
        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = messages.groupingBy { it }.eachCount()
        )

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test many to many store`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            repeat(times = 5) {
                store += MockProducer(store, listOf(4, 8, 15, 16, 23, 42))
            }
            repeat(times = 5) {
                store += MockConsumer(store, messages::add)
            }
        }
        assertTrue(store.isRunning)

        Thread.sleep(500)
        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = messages.groupingBy { it }.eachCount()
        )

        store.stop()
        assertFalse(store.isRunning)
    }

    @RepeatedTest(5)
    fun `test instant stop`() {
        val messages = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            repeat(times = 5) {
                store += MockProducer(
                    store, sequenceOf(4, 8, 15, 16, 23, 42).onEach {
                        Thread.sleep(500)
                    }.asIterable()
                )
            }
            repeat(times = 5) {
                store += MockConsumer(store) {
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

        private class MockProducer(
            private val store: Store<Int>,
            private val producer: Iterable<Int>,
        ) : Producer {
            override fun produce() = producer.forEach(store::send)
        }

        private class MockConsumer(
            private val store: Store<Int>,
            private val consumer: (Int) -> Unit,
        ) : Consumer {
            override fun consume() = generateSequence(store::receive).forEach(consumer)
        }
    }
}
