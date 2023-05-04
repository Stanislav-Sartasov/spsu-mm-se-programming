package deanery.set

import deanery.set.OptimisticSetNode.HeadNode
import deanery.set.OptimisticSetNode.RegNode
import kotlin.concurrent.withLock


class OptimisticSet<T> : ConcurrentSet<T> {

    private val head: OptimisticSetNode<T> = HeadNode()


    @Volatile
    override var count: Int = 0


    override fun contains(element: T): Boolean {
        val key: Int = element.hashCode()

        while (true) {
            findWindow(key).withLock { (pred, curr) ->
                if (validate(pred, curr)) {
                    return curr.key == key
                }
            }
        }
    }

    override fun add(element: T) {
        val key: Int = element.hashCode()

        while (true) {
            findWindow(key).withLock { (pred, curr) ->
                if (validate(pred, curr)) {
                    if (curr.key != key) {
                        pred.next = RegNode(element, curr)
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
            findWindow(key).withLock { (pred, curr) ->
                if (validate(pred, curr)) {
                    if (curr.key == key) {
                        pred.next = curr.next
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

    private fun validate(pred: OptimisticSetNode<*>, curr: OptimisticSetNode<*>): Boolean {
        var node = head
        while (node.key <= pred.key) {
            if (node === pred) return pred.next === curr
            node = node.next
        }
        return false
    }

    private inline fun <T> Window<T>.withLock(b: (Window<T>) -> Unit) =
        first.withLock { second.withLock { b(this) } }
}

private typealias Window<T> = Pair<OptimisticSetNode<T>, OptimisticSetNode<T>>
