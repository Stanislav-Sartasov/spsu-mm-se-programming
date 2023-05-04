package deanery.set

import java.util.concurrent.atomic.AtomicMarkableReference


class NonBlockingSetNode<T : Any>(
    val value: T? = null,
    val key: Int = value.hashCode(),
    next: AtomicMarkableReference<NonBlockingSetNode<T>>? = null,
) {

    lateinit var next: AtomicMarkableReference<NonBlockingSetNode<T>>

    init {
        if (next != null) this.next = next
    }
}
