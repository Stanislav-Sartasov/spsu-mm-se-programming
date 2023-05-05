package deanery.set

import java.util.concurrent.atomic.AtomicMarkableReference


internal class NonBlockingSetNode<T : Any>(
    val value: T? = null,
    val key: Int = value.hashCode(),
    next: NonBlockingSetNode<T>? = null,
) {

    val next = AtomicMarkableReference(next, false)
}
