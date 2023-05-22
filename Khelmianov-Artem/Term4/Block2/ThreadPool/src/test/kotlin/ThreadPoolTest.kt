import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import java.util.concurrent.CyclicBarrier
import java.util.concurrent.atomic.AtomicInteger

class ThreadPoolTest {
    val nThreads = 6
    lateinit var tp: ThreadPool

    @BeforeEach
    fun setUp() {
        tp = ThreadPool(nThreads.toUInt())
    }

    @AfterEach
    fun tearDown() {
        if (tp.isRunning) {
            tp.close()
        }
    }

    @Test
    fun normal() {
        val sum = AtomicInteger(0)
        repeat(nThreads) {
            tp.execute { repeat(10) { sum.incrementAndGet() } }
        }
        Thread.sleep(100)
        assert(sum.get() == nThreads * 10)
    }

    @Test
    fun manyTasks() {
        val sum = AtomicInteger(0)
        repeat(nThreads * 10) {
            tp.execute { repeat(10) { sum.incrementAndGet() } }
        }
        Thread.sleep(100)
        assert(sum.get() == nThreads * 100)
    }

    @Test
    fun executeAfterClose() {
        tp.close()
        assertThrows<IllegalStateException> { tp.execute { } }
    }

    @Test
    fun finishQueuedTasks() {
        val barrier = CyclicBarrier(nThreads + 1)
        repeat(nThreads) {
            tp.execute { barrier.await() }
        }
        repeat(10) { tp.execute { } }
        assert(tp.queueSize == 10)
        barrier.await()
        Thread.sleep(100)
        assert(tp.queueSize == 0)
    }
}