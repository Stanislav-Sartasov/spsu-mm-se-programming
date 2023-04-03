package prodcon

import java.util.concurrent.locks.*
import kotlin.concurrent.thread
import kotlin.concurrent.withLock


class Store<T : Any>(builder: Store<T>.() -> Unit) {

    @Volatile
    var isRunning: Boolean = true
        private set

    private val producers: MutableList<Producer<T>> = mutableListOf()
    private val consumers: MutableList<Consumer<T>> = mutableListOf()

    private val threads = mutableListOf<Thread>()

    private val elements: MutableList<T> = mutableListOf()

    private val lock: Lock = ReentrantLock()
    private val notEmpty: Condition = lock.newCondition()

    init {
        builder()
        run()
    }


    operator fun Producer<T>.unaryPlus() {
        producers += this
    }

    operator fun Consumer<T>.unaryPlus() {
        consumers += this
    }


    private fun run() {
        threads += producers.mapIndexed { i, p ->
            thread(name = "Producer #$i") {
                while (isRunning) send(p.produce())
            }
        }
        threads += consumers.mapIndexed { i, c ->
            thread(name = "Consumer #$i") {
                while (isRunning) c.consume(receive() ?: break)
            }
        }
    }

    private fun send(element: T) = lock.withLock {
        if (isRunning) {
            elements += element
            notEmpty.signal()
        }
    }

    private fun receive(): T? = lock.withLock {
        if (isRunning) {
            var product: T? = elements.removeFirstOrNull()
            while (isRunning && product == null) {
                notEmpty.await()
                product = elements.removeFirstOrNull()
            }
            product
        } else {
            null
        }
    }


    fun stop() {
        lock.withLock {
            isRunning = false
            notEmpty.signalAll()
        }
        threads.forEach(Thread::join)
        threads.clear()
    }
}
