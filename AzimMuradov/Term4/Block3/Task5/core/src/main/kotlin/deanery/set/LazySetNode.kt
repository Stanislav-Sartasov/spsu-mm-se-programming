package deanery.set

import java.util.concurrent.locks.Lock
import java.util.concurrent.locks.ReentrantLock


class LazySetNode<T : Any>(
    val value: T? = null,
    val key: Int = value.hashCode(),
    next: LazySetNode<T>? = null,
) : Lock by ReentrantLock(), Comparable<LazySetNode<T>> {

    lateinit var next: LazySetNode<T>

    var mark: Boolean = false

    init {
        if (next != null) this.next = next
    }

    override fun compareTo(other: LazySetNode<T>): Int = key.compareTo(other.key)
}
