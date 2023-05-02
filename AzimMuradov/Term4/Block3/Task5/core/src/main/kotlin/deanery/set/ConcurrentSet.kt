package deanery.set


interface ConcurrentSet<T> {

    val count: Int


    operator fun contains(element: T): Boolean

    fun add(element: T)

    fun remove(element: T)
}
