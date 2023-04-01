package channels

import org.junit.jupiter.api.Test
import kotlin.concurrent.thread
import kotlin.test.assertContentEquals
import kotlin.test.assertTrue


class StoreConsumerTest {

    @Test
    fun `consume numbers`() {
        val store = object : Store<Int> {

            var cnt = 10

            lateinit var consumer: Consumer

            fun run() {
                thread(name = "consumer", block = consumer::consume)
            }

            @get:Synchronized
            override var isRunning: Boolean = true

            override fun poll(): Int? = if (cnt > 0) cnt-- else null

            override fun offer(element: Int) = error("")

            override fun stop() {
                isRunning = false
            }
        }

        val actual = mutableListOf<Int>()

        store.consumer = StoreConsumer(store) { it.forEach(actual::add) }

        store.run()

        Thread.sleep(5000)

        assertContentEquals(expected = 10 downTo 1, actual = actual)

        store.stop()
    }

    @Test
    fun `consume cannot be started`() {
        val store = object : Store<Int> {

            override val isRunning: Boolean = false

            override fun poll() = error("")

            override fun offer(element: Int) = error("")

            override fun stop() = error("")
        }

        val actual = mutableListOf<Int>()

        StoreConsumer(store) { it.forEach(actual::add) }.consume()

        assertTrue(actual.isEmpty())
    }
}
