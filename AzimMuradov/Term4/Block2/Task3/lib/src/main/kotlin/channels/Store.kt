package channels


interface Store<T> {

    val isRunning: Boolean


    fun offer(element: T)

    fun poll(): T?


    fun stop()
}
