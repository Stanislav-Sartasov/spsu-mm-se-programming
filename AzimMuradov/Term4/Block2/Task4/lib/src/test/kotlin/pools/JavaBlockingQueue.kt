package pools

import java.util.concurrent.*


/**
 * Adapter for the [LinkedBlockingQueue].
 */
class JavaBlockingQueue : BlockingQueue<Runnable> {

    private val queue = LinkedBlockingQueue<Runnable>()


    override val size: Int get() = queue.size


    override fun offer(element: Runnable) = queue.put(element)

    override fun poll(): Runnable? = queue.poll(500, TimeUnit.MILLISECONDS)
}
