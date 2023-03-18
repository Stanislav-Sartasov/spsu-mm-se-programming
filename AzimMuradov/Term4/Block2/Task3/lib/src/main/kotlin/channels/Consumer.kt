package channels


interface Consumer<T> {

    val name: String

    fun consume()
}
