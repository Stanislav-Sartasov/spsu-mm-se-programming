package pools

import java.util.concurrent.*


class BlockingQueue : Queue<Runnable> {

    private val queue = LinkedBlockingQueue<Runnable>()


    override val size: Int get() = queue.size


    override fun peek(): Runnable? = queue.peek()

    override fun offer(element: Runnable) {
        queue.offer(element)
    }

    override fun poll(): Runnable? = queue.poll()
}
