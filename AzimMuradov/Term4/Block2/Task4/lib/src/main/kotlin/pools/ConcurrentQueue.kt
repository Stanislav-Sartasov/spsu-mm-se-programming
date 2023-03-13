package pools


class ConcurrentQueue<T : Any> : Queue<T> {

    private val list = mutableListOf<T>()


    override val size: Int
        @Synchronized
        get() = list.size


    @Synchronized
    override fun getHead(): T? = list.firstOrNull()

    @Synchronized
    override fun enqueue(element: T) {
        list.add(element)
    }

    @Synchronized
    override fun dequeue(): T? = list.removeFirstOrNull()
}
