import java.util.concurrent.Executors
import java.util.concurrent.Semaphore

fun main(args: Array<String>) {
    ProducerConsumerProblem(2, 4).let { pc ->
        pc.start()
        readlnOrNull()
        pc.stop()
    }
}

class ProducerConsumerProblem(
    private val nProds: Int,
    private val nCons: Int
) {
    private val executor = Executors.newVirtualThreadPerTaskExecutor()
    private val list = mutableListOf<Data>()
    private val mutex = Semaphore(1)
    val actors = mutableListOf<Actor<Data>>()

    fun start() {
        repeat(nProds) { actors.add(Producer(list, mutex, "Producer-$it")) }
        repeat(nCons) { actors.add(Consumer(list, mutex, "Consumer-$it")) }
        actors.forEach { executor.submit(it) }
    }

    fun stop() {
        actors.forEach { it.isRunning = false }
        executor.shutdown()
    }
}


fun <T> Semaphore.withLock(block: () -> T): T {
    acquire()
    try {
        return block()
    } finally {
        release()
    }
}
