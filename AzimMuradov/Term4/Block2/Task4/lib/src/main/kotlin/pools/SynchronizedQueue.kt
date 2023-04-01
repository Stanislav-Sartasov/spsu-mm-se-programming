package pools


class SynchronizedQueue<T : Any> : Queue<T> {

    private val list = mutableListOf<T>()


    @get:Synchronized
    override val size: Int get() = list.size


    @Synchronized
    override fun peek(): T? = list.firstOrNull()

    @Synchronized
    override fun offer(element: T) {
        list.add(element)
    }

    @Synchronized
    override fun poll(): T? = list.removeFirstOrNull()
}
