package channels

import org.junit.jupiter.api.Test
import kotlin.concurrent.thread
import kotlin.test.assertContentEquals
import kotlin.test.assertTrue


class StoreProducerTest {

    @Test
    fun `produce numbers`() {
        val store = object : Store<Int> {

            lateinit var producer: Producer

            val products = mutableListOf<Int>()

            fun run() {
                thread(name = "producer", block = producer::produce)
            }

            override var isRunning: Boolean = true

            override fun poll() = error("")

            override fun offer(element: Int) {
                products += element
            }

            override fun stop() {
                isRunning = false
            }
        }

        store.producer = StoreProducer(store, sequenceOf(1, 2, 3))

        store.run()

        Thread.sleep(5000)

        assertContentEquals(expected = listOf(1, 2, 3), actual = store.products)

        store.stop()
    }

    @Test
    fun `produce cannot be started`() {
        val store = object : Store<Int> {

            val products = mutableListOf<Int>()

            override val isRunning: Boolean = false

            override fun poll() = error("")

            override fun offer(element: Int) {
                products += element
            }

            override fun stop() = error("")
        }

        StoreProducer(store, sequenceOf(1, 2, 3)).produce()

        assertTrue(store.products.isEmpty())
    }
}
