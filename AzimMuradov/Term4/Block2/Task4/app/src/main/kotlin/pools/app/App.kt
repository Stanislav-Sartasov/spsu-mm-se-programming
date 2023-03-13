package pools.app

import pools.SynchronizedQueue
import pools.ThreadPool
import kotlin.random.Random
import kotlin.random.nextLong


fun main() {
    ThreadPool.with(threadCount = 4u, SynchronizedQueue()).use { pool ->
        repeat(times = 50) {
            pool.execute {
                Thread.sleep(Random.nextLong(10L..2000L))
                println("${Thread.currentThread().name}: Hello #$it!")
            }
        }
    }
}
