package prodcons


fun interface Consumer<in T : Any> {

    fun consume(message: T)
}
