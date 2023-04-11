package deanery.set


interface ConcurrentSet<T> {

    val count: Int


    fun add(element: T)

    fun remove(element: T)

    operator fun contains(element: T): Boolean
}
