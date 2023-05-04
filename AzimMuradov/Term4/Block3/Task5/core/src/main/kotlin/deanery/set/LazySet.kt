package deanery.set

import kotlin.concurrent.withLock
import deanery.set.LazySetNode as Node


class LazySet<T : Any> : ConcurrentSet<T> {

    private val head = Node<T>(
        key = Int.MIN_VALUE,
        next = Node(key = Int.MAX_VALUE)
    )


    @Volatile
    override var count: Int = 0


    override fun contains(element: T): Boolean {
        val key: Int = element.hashCode()
        val curr = generateSequence(head) { if (it.key < key) it.next else null }.last()
        return curr.key == key && !curr.mark
    }

    override fun add(element: T) {
        val key: Int = element.hashCode()

        while (true) {
            findWindow(key).withLock { (prev, curr) ->
                if (validate(prev, curr)) {
                    if (curr.key != key) {
                        prev.next = Node(element, next = curr)
                        count++
                    }
                    return
                }
            }
        }
    }

    override fun remove(element: T) {
        val key: Int = element.hashCode()

        while (true) {
            findWindow(key).withLock { (prev, curr) ->
                if (validate(prev, curr)) {
                    if (curr.key == key) {
                        curr.mark = true
                        prev.next = curr.next
                        count--
                    }
                    return
                }
            }
        }
    }


    private fun findWindow(key: Int) = generateSequence(seed = head to head.next) { (_, curr) ->
        if (curr.key < key) {
            curr to curr.next
        } else {
            null
        }
    }.last()


    companion object {

        private fun validate(prev: Node<*>, curr: Node<*>): Boolean =
            !prev.mark && !curr.mark && prev.next == curr

        private inline fun <T : Any> OWindow<T>.withLock(b: (OWindow<T>) -> Unit) =
            first.withLock { second.withLock { b(this) } }
    }
}

private typealias OWindow<T> = Pair<Node<T>, Node<T>>
