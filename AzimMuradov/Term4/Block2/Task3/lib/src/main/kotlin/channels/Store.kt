package channels

import kotlin.concurrent.thread


class Store<T>(builder: (Store<T>) -> Unit) {

    @Volatile
    var isRunning: Boolean = true
        private set

    private val producers: MutableList<Producer<T>> = mutableListOf()
    private val consumers: MutableList<Consumer<T>> = mutableListOf()

    @get:Synchronized
    private val elements: MutableList<T> = mutableListOf()

    init {
        builder(this)
        run()
    }


    internal operator fun plusAssign(producer: Producer<T>) {
        producers += producer
    }

    internal operator fun plusAssign(consumer: Consumer<T>) {
        consumers += consumer
    }


    internal fun offer(element: T) = synchronized(elements) {
        elements += element
    }

    internal fun poll(): T? = synchronized(elements) {
        elements.removeFirstOrNull()
    }


    private fun run() {
        producers.forEach {
            thread(name = it.name) { it.produce() }
        }
        consumers.forEach {
            thread(name = it.name) { it.consume() }
        }
    }

    fun stop() {
        synchronized(elements) {
            isRunning = false
        }
    }
}
