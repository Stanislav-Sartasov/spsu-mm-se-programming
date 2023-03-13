package pools.app

import pools.ConcurrentQueue
import pools.StdLibQueue
import pools.ThreadPool
import java.util.concurrent.*


fun main() {
    val q: StdLibQueue<Runnable> = StdLibQueue(LinkedBlockingQueue())
    ThreadPool.with(threadCount = 4u, queue = ConcurrentQueue()).use { pool ->
        repeat(times = 10) {
            pool.execute {
                Thread.sleep(2000)
                println("Hello #$it")
            }
        }
    }
}
