package pools


interface Queue<T : Any> {

    val size: Int


    fun peek(): T?

    fun offer(element: T)

    fun poll(): T?
}

fun Queue<*>.isEmpty() = size == 0

fun Queue<*>.isNotEmpty() = size != 0
