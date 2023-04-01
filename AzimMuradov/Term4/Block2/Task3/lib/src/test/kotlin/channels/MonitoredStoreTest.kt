package channels

import org.junit.jupiter.api.Test
import java.util.concurrent.*
import kotlin.test.assertContentEquals
import kotlin.test.assertEquals
import kotlin.test.assertFalse
import kotlin.test.assertTrue


class MonitoredStoreTest {

    @Test
    fun `test empty store`() {
        val store = MonitoredStore<Int> { }
        assertTrue(store.isRunning)
        store.stop()
        assertFalse(store.isRunning)
    }

    @Test
    fun `test 1 to 1 store`() {
        val accumulated = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            store += MockProducer(store, sequenceOf(4, 8, 15, 16, 23, 42))
            store += MockConsumer(store) { it.forEach(accumulated::add) }
        }

        assertTrue(store.isRunning)

        Thread.sleep(500)

        assertContentEquals(
            expected = listOf(4, 8, 15, 16, 23, 42),
            actual = accumulated
        )
        store.stop()

        assertFalse(store.isRunning)
    }

    @Test
    fun `test 1 to many store`() {
        val accumulated = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            store += MockProducer(store, sequenceOf(4, 8, 15, 16, 23, 42))
            repeat(times = 5) {
                store += MockConsumer(store) { products ->
                    products.forEach {
                        assertTrue(accumulated.add(it))
                    }
                }
            }
        }

        assertTrue(store.isRunning)

        Thread.sleep(500)

        assertEquals(accumulated.size, accumulated.distinct().size)

        assertEquals(
            expected = setOf(4, 8, 15, 16, 23, 42),
            actual = accumulated.toSet()
        )
        store.stop()

        assertFalse(store.isRunning)
    }

    @Test
    fun `test many to 1 store`() {
        val accumulated = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            repeat(times = 5) {
                store += MockProducer(store, sequenceOf(4, 8, 15, 16, 23, 42))
            }
            store += MockConsumer(store) { it.forEach(accumulated::add) }
        }

        assertTrue(store.isRunning)

        Thread.sleep(500)

        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = accumulated.groupingBy { it }.eachCount()
        )
        store.stop()

        assertFalse(store.isRunning)
    }

    @Test
    fun `test many to many store`() {
        val accumulated = LinkedBlockingQueue<Int>()
        val store = MonitoredStore { store ->
            repeat(times = 5) {
                store += MockProducer(store, sequenceOf(4, 8, 15, 16, 23, 42))
            }
            repeat(times = 5) {
                store += MockConsumer(store) { it.forEach(accumulated::add) }
            }
        }

        assertTrue(store.isRunning)

        Thread.sleep(500)

        assertEquals(
            expected = listOf(4, 8, 15, 16, 23, 42).associateWith { 5 },
            actual = accumulated.groupingBy { it }.eachCount()
        )
        store.stop()

        assertFalse(store.isRunning)
    }


    companion object {

        private class MockProducer(
            private val store: Store<Int>,
            private val producer: Sequence<Int>,
        ) : Producer {

            override fun produce() {
                if (!store.isRunning) return
                producer.forEach {
                    store.offer(element = it)
                    if (!store.isRunning) return
                }
            }
        }

        private class MockConsumer(
            private val store: Store<Int>,
            private val consumer: (Sequence<Int>) -> Unit,
        ) : Consumer {

            override fun consume() = consumer(
                generateSequence {
                    if (store.isRunning) {
                        var product: Int?
                        do product = store.poll()
                        while (store.isRunning && product == null)
                        product
                    } else {
                        null
                    }
                }
            )
        }
    }
}
