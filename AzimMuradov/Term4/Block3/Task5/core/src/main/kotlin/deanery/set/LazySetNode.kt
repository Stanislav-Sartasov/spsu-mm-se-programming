package deanery.set

import java.util.concurrent.locks.Lock
import java.util.concurrent.locks.ReentrantLock


internal class LazySetNode<T : Any>(
    val value: T? = null,
    val key: Int = value.hashCode(),
    next: LazySetNode<T>? = null,
) : Lock by ReentrantLock() {

    lateinit var next: LazySetNode<T>

    var mark: Boolean = false

    init {
        if (next != null) this.next = next
    }
}
