package deanery.set

import java.util.concurrent.locks.Lock
import java.util.concurrent.locks.ReentrantLock


internal sealed class OptimisticSetNode<T> : Lock by ReentrantLock(), Comparable<OptimisticSetNode<T>> {

    abstract val key: Int

    abstract var next: OptimisticSetNode<T>


    class HeadNode<T> : OptimisticSetNode<T>() {

        override val key: Int = Int.MIN_VALUE

        override var next: OptimisticSetNode<T> = TailNode()
    }

    class RegNode<T>(val value: T, override var next: OptimisticSetNode<T>) : OptimisticSetNode<T>() {

        override val key: Int = value.hashCode()
    }

    class TailNode<T> : OptimisticSetNode<T>() {

        override val key: Int = Int.MAX_VALUE

        override var next: OptimisticSetNode<T>
            get() = error("Inaccessible")
            set(_) {}
    }

    override fun compareTo(other: OptimisticSetNode<T>): Int = key.compareTo(other.key)
}
