package channels

import java.util.concurrent.locks.*
import kotlin.concurrent.thread
import kotlin.concurrent.withLock


class MonitoredStore<T : Any>(builder: (MonitoredStore<T>) -> Unit) : Store<T> {

    @Volatile
    override var isRunning: Boolean = true
        private set

    private val producers: MutableList<Producer> = mutableListOf()
    private val consumers: MutableList<Consumer> = mutableListOf()

    private val threads = mutableListOf<Thread>()

    private val elements: MutableList<T> = mutableListOf()

    private val lock: Lock = ReentrantLock()
    private val condition: Condition = lock.newCondition()

    init {
        builder(this)
        run()
    }


    operator fun plusAssign(producer: Producer) {
        producers += producer
    }

    operator fun plusAssign(consumer: Consumer) {
        consumers += consumer
    }


    override fun send(element: T) = lock.withLock {
        if (isRunning) {
            elements += element
            condition.signal()
        }
        isRunning
    }

    override fun receive(): T? = lock.withLock {
        if (isRunning) {
            var product: T? = elements.removeFirstOrNull()
            while (isRunning && product == null) {
                condition.await()
                product = elements.removeFirstOrNull()
            }
            product
        } else {
            null
        }
    }

    private fun run() {
        threads += producers.mapIndexed { i, p ->
            thread(name = "Producer #$i", block = p::produce)
        }
        threads += consumers.mapIndexed { i, c ->
            thread(name = "Consumer #$i", block = c::consume)
        }
    }

    override fun stop() {
        lock.withLock {
            isRunning = false
            condition.signalAll()
        }
        threads.forEach(Thread::join)
        threads.clear()
    }
}
