package prodcon


fun interface Consumer<in T : Any> {

    fun consume(message: T)
}
