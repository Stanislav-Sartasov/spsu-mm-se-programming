package channels

import java.util.concurrent.locks.*
import kotlin.concurrent.thread
import kotlin.concurrent.withLock


class Store<T>(builder: (Store<T>) -> Unit) {

    @Volatile
    var isRunning: Boolean = true
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


    internal operator fun plusAssign(producer: Producer) {
        producers += producer
    }

    internal operator fun plusAssign(consumer: Consumer) {
        consumers += consumer
    }


    internal fun offer(element: T) = lock.withLock {
        elements += element
        condition.signal()
    }

    internal fun poll(): T? = lock.withLock {
        elements.removeFirstOrNull().also {
            if (it == null) condition.await()
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

    fun stop() = lock.withLock {
        isRunning = false
        condition.signalAll()
    }
}
