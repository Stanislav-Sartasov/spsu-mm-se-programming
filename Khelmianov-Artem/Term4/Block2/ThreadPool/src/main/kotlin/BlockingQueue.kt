import java.util.*
import java.util.concurrent.Semaphore
import java.util.concurrent.TimeUnit
import java.util.concurrent.locks.ReentrantLock
import kotlin.concurrent.withLock

class BlockingQueue<T> : AbstractQueue<T>() {
    private val storage = mutableListOf<T>()
    override val size: Int
        get() = storage.size
    private val lock = ReentrantLock()
    private val onReceive = lock.newCondition()

    override fun offer(e: T): Boolean = lock.withLock {
        onReceive.signal()
        storage.add(e)
    }

    override fun poll(): T? = lock.withLock {
        if (storage.isEmpty()) {
            onReceive.await(10, TimeUnit.MILLISECONDS)
        }
        storage.removeFirstOrNull()
    }


    override fun peek(): T = TODO("Not needed")

    override fun iterator(): MutableIterator<T> = TODO("Not needed")
}

