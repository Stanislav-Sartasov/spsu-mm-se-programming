package prodcons


fun interface Producer<out T : Any> {

    fun produce(): T
}
