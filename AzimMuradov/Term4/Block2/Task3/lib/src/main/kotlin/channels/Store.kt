package channels

import kotlin.concurrent.thread


class Store<T>(builder: (Store<T>) -> Unit) {

    @Volatile
    var isRunning: Boolean = true
        private set

    private val producers: MutableList<Producer> = mutableListOf()
    private val consumers: MutableList<Consumer> = mutableListOf()

    private val elements: MutableList<T> = mutableListOf()

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


    @Synchronized
    internal fun offer(element: T) {
        elements += element
    }

    @Synchronized
    internal fun poll(): T? = elements.removeFirstOrNull()


    private fun run() {
        for ((i, p) in producers.withIndex()) {
            thread(name = "Producer #$i", block = p::produce)
        }
        for ((i, c) in consumers.withIndex()) {
            thread(name = "Consumer #$i", block = c::consume)
        }
    }

    @Synchronized
    fun stop() {
        isRunning = false
    }
}
