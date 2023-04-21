package pools


interface BlockingQueue<T : Any> {

    val size: Int


    fun offer(element: T)

    fun poll(): T?
}

fun BlockingQueue<*>.isEmpty() = size == 0

fun BlockingQueue<*>.isNotEmpty() = size != 0
