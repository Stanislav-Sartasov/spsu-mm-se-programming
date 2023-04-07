package prodcon


fun interface Producer<out T : Any> {

    fun produce(): T
}
