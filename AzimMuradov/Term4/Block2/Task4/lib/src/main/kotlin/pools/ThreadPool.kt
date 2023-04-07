package pools

import java.util.concurrent.*


class ThreadPool private constructor(
    val threadCount: UInt,
    private val workQueue: BlockingQueue<Runnable>,
) : AutoCloseable, Executor {

    private var isRunning = true

    private var threads: List<Thread>

    init {
        poolCount++
        threads = List(threadCount.toInt()) { i ->
            ThreadPoolThread(
                name = "ThreadPool #$poolCount - Thread #$i",
                isRunning = ::isRunning,
                workQueue = workQueue
            ).apply(Thread::start)
        }
    }


    override fun execute(runnable: Runnable) {
        check(isRunning) { "Unable to execute, ThreadPool was closed" }
        workQueue.offer(runnable)
    }

    override fun close() {
        isRunning = false
        threads.forEach(Thread::join)
        threads = emptyList()
    }


    companion object {

        fun with(threadCount: UInt, workQueue: BlockingQueue<Runnable>) = ThreadPool(threadCount, workQueue)

        private var poolCount = 0
    }
}
