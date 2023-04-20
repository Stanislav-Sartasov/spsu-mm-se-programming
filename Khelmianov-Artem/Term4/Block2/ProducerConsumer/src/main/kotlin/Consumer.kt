import java.util.concurrent.Semaphore
import java.util.concurrent.locks.ReentrantLock
import kotlin.random.Random

class Consumer(
    queue: MutableList<Data>,
    mutex: ReentrantLock,
    name: String = ""
) : Actor<Data>(queue, mutex, name) {
    override fun run() {
        isRunning = true
        while (isRunning) {
            mutex.withLock {
                if (!isRunning) return@withLock
                queue.removeFirstOrNull()?.let {
                    println("$name consumed $it")
                }
            }
            Thread.sleep(Random.nextLong(1000, 2000))
        }
    }
}
