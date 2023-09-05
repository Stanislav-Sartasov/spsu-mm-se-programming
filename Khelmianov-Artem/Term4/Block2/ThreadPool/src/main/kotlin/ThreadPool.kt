import java.util.concurrent.Executor
import java.util.concurrent.locks.ReentrantLock
import kotlin.concurrent.withLock

class ThreadPool(nThreads: UInt) : AutoCloseable, Executor {
    @Volatile
    var isRunning = true
    private val threads = List(nThreads.toInt()) { Thread(Worker()) }
    private val queue = BlockingQueue<Runnable>()
    private val lock = ReentrantLock()
    private val onReceive = lock.newCondition()
    val queueSize: Int
        get() = queue.size

    init {
        threads.forEach(Thread::start)
    }

    override fun execute(command: Runnable) {
        if (!isRunning) throw IllegalStateException()
        queue.offer(command)
        lock.withLock { onReceive.signal() }
    }

    override fun close() {
        isRunning = false
        lock.withLock { onReceive.signalAll() }
        threads.forEach(Thread::join)
    }

    inner class Worker : Runnable {
        override fun run() {
            while (true) {
                var task: Runnable? = queue.poll()
                while (task != null) {
                    task.run()
                    task = queue.poll()
                }
                if (!isRunning) return
                lock.withLock { onReceive.await() }
            }
        }
    }
}