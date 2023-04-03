package pools

import java.util.concurrent.*
import java.util.concurrent.locks.*
import kotlin.concurrent.withLock


class BlockingQueueImpl<T : Any> : BlockingQueue<T> {

    private val lock: Lock = ReentrantLock()
    private val notEmpty: Condition = lock.newCondition()
    private val elements = mutableListOf<T>()


    override val size: Int get() = lock.withLock { elements.size }


    override fun offer(element: T) = lock.withLock {
        elements += element
        notEmpty.signal()
    }

    override fun poll(): T? = lock.withLock {
        if (elements.isEmpty()) {
            notEmpty.await(500, TimeUnit.MILLISECONDS)
        }
        elements.removeFirstOrNull()
    }
}
