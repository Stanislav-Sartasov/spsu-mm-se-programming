package channels

import java.util.concurrent.locks.*
import kotlin.concurrent.thread
import kotlin.concurrent.withLock


class MonitoredStore<T>(builder: (MonitoredStore<T>) -> Unit) : Store<T> {

    @Volatile
    override var isRunning: Boolean = true
        private set

    private val producers: MutableList<Producer> = mutableListOf()
    private val consumers: MutableList<Consumer> = mutableListOf()

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


    override fun offer(element: T) = lock.withLock {
        if (isRunning) {
            elements += element
            condition.signal()
        }
    }

    override fun poll(): T? = lock.withLock {
        if (isRunning) {
            elements.removeFirstOrNull().also {
                if (it == null) condition.await()
            }
        } else {
            null
        }
    }


    private fun run() {
        for ((i, p) in producers.withIndex()) {
            thread(name = "Producer #$i", block = p::produce)
        }
        for ((i, c) in consumers.withIndex()) {
            thread(name = "Consumer #$i", block = c::consume)
        }
    }

    override fun stop() = lock.withLock {
        isRunning = false
        condition.signalAll()
    }
}
