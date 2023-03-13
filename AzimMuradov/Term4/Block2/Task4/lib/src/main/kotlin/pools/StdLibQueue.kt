package pools

import java.util.Queue as JavaQueue


class StdLibQueue<T : Any>(private val queue: JavaQueue<T>) : Queue<T> {

    override val size: Int get() = queue.size

    override fun getHead(): T? = queue.peek()

    override fun enqueue(element: T) {
        queue.offer(element)
    }

    override fun dequeue(): T? = queue.poll()
}
