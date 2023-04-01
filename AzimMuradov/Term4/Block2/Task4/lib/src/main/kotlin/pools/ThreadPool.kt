package pools

import java.util.concurrent.*


class ThreadPool private constructor(
    val threadCount: UInt,
    private val workQueue: Queue<Runnable>,
) : AutoCloseable, Executor {

    private var isRunning = true

    init {
        poolCount++
        repeat(threadCount.toInt()) { i ->
            ThreadPoolThread(
                name = "ThreadPool #$poolCount - Thread #$i",
                isRunning = ::isRunning,
                workQueue = workQueue
            ).start()
        }
    }


    override fun execute(runnable: Runnable) {
        check(isRunning) { "Unable to execute, ThreadPool was closed" }
        workQueue.offer(runnable)
    }

    override fun close() {
        isRunning = false
    }


    companion object {

        fun with(threadCount: UInt, workQueue: Queue<Runnable>) = ThreadPool(threadCount, workQueue)

        private var poolCount = 0
    }
}
