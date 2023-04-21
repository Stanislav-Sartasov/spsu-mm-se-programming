package pools

import kotlinx.atomicfu.atomic
import org.junit.jupiter.api.RepeatedTest
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import java.util.*
import kotlin.test.assertEquals


class ThreadPoolTest {

    @Test
    fun `test thread pool with size 0`() {
        val q = JavaBlockingQueue()

        ThreadPool.with(threadCount = 0u, workQueue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 0u, actual = pool.threadCount)

            pool.execute { error("unreachable") }

            assertEquals(expected = 1, actual = q.size)
        }
    }

    @RepeatedTest(5)
    fun `test thread pool with size 1`() {
        val q = JavaBlockingQueue()

        val cnt = atomic(0)

        ThreadPool.with(threadCount = 1u, workQueue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 1u, actual = pool.threadCount)

            repeat(times = 10000) { pool.execute(cnt::incrementAndGet) }
        }

        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = 10000, actual = cnt.value)
    }

    @RepeatedTest(5)
    fun `test thread pool with size 2`() {
        val q = JavaBlockingQueue()

        val cnt = atomic(0)

        ThreadPool.with(threadCount = 2u, workQueue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 2u, actual = pool.threadCount)

            repeat(times = 10000) { pool.execute(cnt::incrementAndGet) }
        }

        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = 10000, actual = cnt.value)
    }

    @RepeatedTest(5)
    fun `test thread pool with size 5`() {
        val q = JavaBlockingQueue()

        val cnt = atomic(0)

        ThreadPool.with(threadCount = 5u, workQueue = q).use { pool ->
            assertEquals(expected = 0, actual = q.size)
            assertEquals(expected = 5u, actual = pool.threadCount)

            repeat(times = 10000) { pool.execute(cnt::incrementAndGet) }
        }

        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = 10000, actual = cnt.value)
    }

    @Test
    fun `fail on execution after close`() {
        ThreadPool.with(threadCount = 1u, JavaBlockingQueue()).run {
            close()
            assertThrows<IllegalStateException>(
                message = "Unable to execute, ThreadPool was closed"
            ) { execute {} }
        }
    }
}
