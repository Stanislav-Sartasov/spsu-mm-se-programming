package channels


interface Producer<T> {

    val name: String

    fun produce()
}
