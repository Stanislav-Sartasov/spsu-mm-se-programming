package deanery.set


interface ConcurrentSet<T : Any> {

    val count: Int


    operator fun contains(element: T): Boolean

    fun add(element: T)

    fun remove(element: T)
}
