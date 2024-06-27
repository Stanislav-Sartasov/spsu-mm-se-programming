package threadpool

import java.util.LinkedList
import java.util.concurrent.locks.Condition
import java.util.concurrent.locks.ReentrantLock
import kotlin.concurrent.withLock

class WorkQueue<T> {
    private val queue = LinkedList<T>()
    private val lock = ReentrantLock()
    private val notEmpty: Condition = lock.newCondition()

    fun enqueue(item: T) {
        lock.lock()
        try {
            queue.add(item)
            notEmpty.signal()
        } finally {
            lock.unlock()
        }
    }

    fun dequeue(): T? {
        lock.lock()
        try {
            while (queue.isEmpty()) {
                notEmpty.await()
            }
            return queue.removeFirst()
        } finally {
            lock.unlock()
        }
    }

    fun size(): Int {
        lock.lock()
        try {
            return queue.size
        } finally {
            lock.unlock()
        }
    }
}