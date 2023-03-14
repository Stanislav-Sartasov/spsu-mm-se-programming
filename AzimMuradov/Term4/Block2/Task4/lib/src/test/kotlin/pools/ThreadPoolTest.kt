package pools

import kotlin.test.Test
import kotlin.test.assertEquals


class ThreadPoolTest {

    @Test
    fun `test thread pool with size 0`() {
        val q = BlockingQueue()
        ThreadPool.with(threadCount = 0u, queue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 0u, actual = pool.threadCount)

            pool.execute { error("unreachable") }

            assertEquals(expected = 1, actual = q.size)
        }
    }

    @Test
    fun `test thread pool with size 1`() {
        val q = BlockingQueue()

        ThreadPool.with(threadCount = 1u, queue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 1u, actual = pool.threadCount)

            repeat(times = 5) {
                pool.execute { Thread.sleep(500) }
            }

            assertEquals(expected = 5, actual = q.size)
        }

        Thread.sleep(4000)

        assertEquals(expected = 0, actual = q.size)
    }

    @Test
    fun `test thread pool with size 2`() {
        val q = BlockingQueue()

        ThreadPool.with(threadCount = 2u, queue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 2u, actual = pool.threadCount)

            repeat(times = 5) {
                pool.execute { Thread.sleep(1000) }
            }

            assertEquals(expected = 5, actual = q.size)
        }

        Thread.sleep(4000)

        assertEquals(expected = 0, actual = q.size)
    }

    @Test
    fun `test thread pool with size 5`() {
        val q = BlockingQueue()

        ThreadPool.with(threadCount = 5u, queue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 5u, actual = pool.threadCount)

            repeat(times = 5) {
                pool.execute { Thread.sleep(2000) }
            }

            assertEquals(expected = 5, actual = q.size)
        }

        Thread.sleep(4000)

        assertEquals(expected = 0, actual = q.size)
    }
}
