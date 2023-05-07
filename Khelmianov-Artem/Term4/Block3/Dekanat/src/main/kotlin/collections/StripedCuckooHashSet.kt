package collections

import java.util.concurrent.locks.ReentrantLock

class StripedCuckooHashSet<T : Any>(capacity: Int = 64) : PhasedCuckooHashSet<T>(capacity) {

    private val locks = Array(2) { Array(capacity) { ReentrantLock() } }

    override fun acquire(element: T) {
        locks[0, element.hash(0, modulus = locks.size)].lock()
        locks[1, element.hash(1, modulus = locks.size)].lock()
    }

    override fun release(element: T) {
        locks[0, element.hash(0, modulus = locks.size)].unlock()
        locks[1, element.hash(1, modulus = locks.size)].unlock()
    }

    override fun resize() {
        val oldCapacity = capacity
        locks[0].forEach { it.lock() }
        try {
            if (capacity != oldCapacity) return
            val oldStorage = storage
            capacity *= 2
            storage = Array(2) { Array(capacity) { mutableListOf<T>() } }
            oldStorage.forEach { list -> list.forEach { this.add(it) } }
        } finally {
            locks[0].forEach { it.unlock() }
        }
    }
}