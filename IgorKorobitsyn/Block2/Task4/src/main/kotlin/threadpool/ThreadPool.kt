package threadpool

import java.util.concurrent.Executor
import java.util.concurrent.RejectedExecutionException
import java.util.concurrent.TimeUnit
import java.util.concurrent.atomic.AtomicBoolean

class ThreadPool(private val poolSize: Int) : Executor, AutoCloseable {
    private val workQueue = WorkQueue<Runnable>()
    private val workers = mutableListOf<Worker>()
    private val isShutdown = AtomicBoolean(false)

    init {
        for (i in 0 until poolSize) {
            val worker = Worker()
            workers.add(worker)
            worker.start()
        }
    }

    override fun execute(command: Runnable) {
        if (isShutdown.get()) {
            throw RejectedExecutionException("ThreadPool is already shut down")
        }
        workQueue.enqueue(command)
    }

    override fun close() {
        isShutdown.set(true)
        workers.forEach { it.interrupt() }
    }

    private inner class Worker : Thread() {
        override fun run() {
            while (!isShutdown.get()) {
                try {
                    val task = workQueue.dequeue()
                    task?.run()
                } catch (e: InterruptedException) {
                    // Thread interrupted, exit
                    break
                }
            }
        }
    }
}
