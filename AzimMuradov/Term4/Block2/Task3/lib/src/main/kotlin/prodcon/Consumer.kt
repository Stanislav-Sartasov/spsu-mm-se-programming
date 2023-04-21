package prodcon


fun interface Consumer<T : Any> {

    fun consume(message: T)
}
