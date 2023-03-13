package pools


interface Queue<T : Any> {

    val size: Int


    fun getHead(): T?

    fun enqueue(element: T)

    fun dequeue(): T?
}
