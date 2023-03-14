package pools

import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import kotlin.test.Test
import kotlin.test.assertEquals


class SynchronizedQueueTest {

    @Test
    fun `empty queue`() {
        val q = SynchronizedQueue<Int>()
        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = true, actual = q.isEmpty())
        assertEquals(expected = false, actual = q.isNotEmpty())
        assertEquals(expected = null, actual = q.peek())
        assertEquals(expected = null, actual = q.poll())
    }

    @Test
    fun `offer element to queue 1 time`() {
        val q = SynchronizedQueue<Int>().apply { offer(element = 17) }

        assertEquals(expected = 1, actual = q.size)
        assertEquals(expected = false, actual = q.isEmpty())
        assertEquals(expected = true, actual = q.isNotEmpty())
        assertEquals(expected = 17, actual = q.peek())
        assertEquals(expected = 17, actual = q.poll())

        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = true, actual = q.isEmpty())
        assertEquals(expected = false, actual = q.isNotEmpty())
        assertEquals(expected = null, actual = q.peek())
        assertEquals(expected = null, actual = q.poll())
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2, 3, 4, 10])
    fun `offer elements to queue n times`(n: Int) {
        val q = SynchronizedQueue<Int>()

        repeat(times = n) {
            q.offer(it)
            assertEquals(expected = it + 1, actual = q.size)
            assertEquals(expected = false, actual = q.isEmpty())
            assertEquals(expected = true, actual = q.isNotEmpty())
            assertEquals(expected = 0, actual = q.peek())
        }

        repeat(times = n - 1) {
            assertEquals(expected = it, actual = q.poll())
            assertEquals(expected = n - 1 - it, actual = q.size)
            assertEquals(expected = false, actual = q.isEmpty())
            assertEquals(expected = true, actual = q.isNotEmpty())
            assertEquals(expected = it + 1, actual = q.peek())
        }

        assertEquals(expected = n - 1, actual = q.poll())
        assertEquals(expected = 0, actual = q.size)
        assertEquals(expected = true, actual = q.isEmpty())
        assertEquals(expected = false, actual = q.isNotEmpty())
        assertEquals(expected = null, actual = q.peek())
        assertEquals(expected = null, actual = q.poll())
    }
}
