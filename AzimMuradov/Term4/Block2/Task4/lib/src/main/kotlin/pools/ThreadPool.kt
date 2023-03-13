@file:Suppress("PLATFORM_CLASS_MAPPED_TO_KOTLIN")

package pools

import java.util.concurrent.*
import kotlin.concurrent.thread


class ThreadPool private constructor(
    private val threadCount: UInt,
    private val workQueue: Queue<Runnable>,
) : Executor, AutoCloseable {

    @Volatile
    private var threads: List<Thread> = run {
        val taskWorker = {
            while (isRunning) {
                workQueue.dequeue()?.run()
            }
        }
        List(threadCount.toInt()) { thread(block = taskWorker) }
    }

    @Volatile
    private var isRunning = true


    override fun execute(command: Runnable) {
        // synchronized(workQueue) {
            if (isRunning) {
                workQueue.enqueue(command)
                // (workQueue as Object).notify()
            }
        // }
    }


    override fun close() {
        isRunning = false
        synchronized(workQueue) { (workQueue as Object).notifyAll() }
        for (thread in threads) {
            try {
                thread.join()
            } catch (e: InterruptedException) {
                e.printStackTrace()
            }
        }
    }


    companion object {

        fun with(threadCount: UInt, queue: Queue<Runnable>) = ThreadPool(threadCount, queue)
    }
}
