package deanery.set

import java.util.concurrent.atomic.AtomicInteger
import deanery.set.NonBlockingSetNode as Node


class NonBlockingSet<T : Any> : ConcurrentSet<T> {

    private val head = Node<T>(
        key = Int.MIN_VALUE,
        next = Node(key = Int.MAX_VALUE)
    )


    private var _count = AtomicInteger()

    override val count: Int get() = _count.get()


    override fun contains(element: T): Boolean {
        val marked = booleanArrayOf(false)
        val key = element.hashCode()
        var curr = head
        while (curr.key < key) {
            curr = curr.next.reference!!
            curr.next.get(marked)
        }
        return curr.key == key && !marked[0]
    }

    override fun add(element: T) {
        val key: Int = element.hashCode()
        while (true) {
            val (pred, curr) = find(key)
            if (curr.key != key) {
                val node = Node(element, next = curr)
                if (pred.next.compareAndSet(curr, node, false, false)) {
                    _count.incrementAndGet()
                    return
                }
            } else {
                return
            }
        }
    }

    override fun remove(element: T) {
        val key: Int = element.hashCode()
        while (true) {
            val (prev, curr) = find(key)
            if (curr.key == key) {
                val next = curr.next.reference
                if (curr.next.attemptMark(next, true)) {
                    prev.next.compareAndSet(curr, next, false, false)
                    _count.decrementAndGet()
                    return
                }
            } else {
                return
            }
        }
    }


    private fun find(key: Int): NBWindow<T> {
        var prev: Node<T>
        var curr: Node<T>
        var next: Node<T>?
        val mark = booleanArrayOf(false)

        retry@ while (true) {
            prev = head
            curr = prev.next.reference!!
            while (true) {
                next = curr.next.get(mark)
                while (mark[0]) {
                    if (!prev.next.compareAndSet(curr, next, false, false)) {
                        continue@retry
                    }
                    curr = next!!
                    next = curr.next.get(mark)
                }
                if (curr.key >= key) return prev to curr
                prev = curr
                curr = next!!
            }
        }
    }
}

private typealias NBWindow<T> = Pair<Node<T>, Node<T>>
