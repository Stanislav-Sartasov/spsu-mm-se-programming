package collections

import java.util.concurrent.atomic.AtomicInteger
import java.util.concurrent.atomic.AtomicMarkableReference

class LockFreeListSet<T> : ConcurrentSet<T> {
    private val _size = AtomicInteger(0)
    private val head = Node<T>(null, Int.MIN_VALUE, Node<T>(null, Int.MAX_VALUE))

    override val size: Int get() = _size.get()


    override fun add(element: T): Boolean {
        val key = element.hashCode()
        while (true) {
            findAndRun(key) { (prev, curr) ->
                if (curr.key == key && !curr.isMarked) {
                    return false
                }
                val node = Node(value = element, next = curr)
                if (prev.compareAndSet(curr, node, false, false)) {
                    _size.incrementAndGet()
                    return true
                }
            }
        }
    }

    override fun remove(element: T): Boolean {
        val key = element.hashCode()
        while (true) {
            findAndRun(key) { (pred, curr) ->
                if (curr.key != key)
                    return false

                val succ = curr.reference
                if (!curr.compareAndSet(succ, succ, false, true))
                    return@findAndRun
                pred.compareAndSet(curr, succ, false, false)
                _size.decrementAndGet()
                return true
            }
        }
    }

    override fun contains(element: T): Boolean {
        val key = element.hashCode()
        val curr = find(key).second
        return curr.key == key //&& !curr.isMarked
    }

    private fun find(key: Int): Pair<Node<T>, Node<T>> {
        val marked = booleanArrayOf(false)

        start@ while (true) {
            var pred = head
            var curr = head.reference
            while (true) {
                var succ = curr.get(marked)
                while (marked[0]) {
                    if (!pred.compareAndSet(curr, succ, false, false)) {
                        continue@start
                    }
                    curr = succ
                    succ = curr.get(marked)
                }
                if (curr.key >= key)
                    return Pair(pred, curr)
                pred = curr
                curr = succ
            }
        }
    }

    private inline fun findAndRun(key: Int, block: (Pair<Node<T>, Node<T>>) -> Unit) = block(find(key))
}

private class Node<T>(
    val value: T?,
    val key: Int = value.hashCode(),
    next: Node<T>? = null
) : AtomicMarkableReference<Node<T>>(next, false)




