import java.util.concurrent.locks.ReentrantLock
import kotlin.random.Random

class Producer(
    queue: MutableList<Data>,
    mutex: ReentrantLock,
    name: String = ""
) : Actor<Data>(queue, mutex, name) {
    private var cnt = 0

    override fun run() {
        isRunning = true
        while (isRunning) {
            val item = Data("$name-${cnt++}")
            mutex.withLock {
                if (!isRunning) return@withLock
                queue.add(item)
                println("$name produced $item")
            }
            Thread.sleep(Random.nextLong(1000, 2000))
        }
    }
}
