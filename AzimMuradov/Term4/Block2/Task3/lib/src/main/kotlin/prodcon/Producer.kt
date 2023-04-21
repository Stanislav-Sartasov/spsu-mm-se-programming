package prodcon


fun interface Producer<T : Any> {

    fun produce(): T
}
