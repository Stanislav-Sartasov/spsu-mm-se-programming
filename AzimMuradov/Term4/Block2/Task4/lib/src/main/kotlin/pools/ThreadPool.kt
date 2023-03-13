package pools

import kotlinx.atomicfu.atomic
import java.util.concurrent.*


class ThreadPool private constructor(
    private val threadCount: UInt,
    private val workQueue: Queue<Runnable>,
) : AutoCloseable, Executor {

    private val executeAtomic = atomic(initial = true)

    private var execute by executeAtomic

    init {
        poolCount.incrementAndGet()
    }

    private val threads: List<Thread> = List(threadCount.toInt()) { i ->
        ThreadPoolThread(
            name = "ThreadPool #${poolCount.value} - Thread #$i",
            execute = executeAtomic,
            runnables = workQueue
        ).apply(Thread::start)
    }


    override fun execute(runnable: Runnable) {
        check(execute) { "ThreadPool terminating, unable to execute runnable" }
        workQueue.offer(runnable)
    }

    override fun close() {
        execute = false
    }


    companion object {

        fun with(threadCount: UInt, queue: Queue<Runnable>) = ThreadPool(threadCount, queue)

        private val poolCount = atomic(initial = 0)
    }
}
