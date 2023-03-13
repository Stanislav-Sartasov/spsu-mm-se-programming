package pools


class JavaQueue<T : Any>(private val queue: java.util.Queue<T>) : Queue<T> {

    override val size: Int get() = queue.size


    override fun peek(): T? = queue.peek()

    override fun offer(element: T) {
        queue.offer(element)
    }

    override fun poll(): T? = queue.poll()
}
