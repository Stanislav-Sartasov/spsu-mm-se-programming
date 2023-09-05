package collections

interface ConcurrentSet<T> {
    val size: Int
    fun add(element: T): Boolean
    fun remove(element: T): Boolean
    fun contains(element: T): Boolean
}
