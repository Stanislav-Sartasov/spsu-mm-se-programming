package pools.app

import pools.BlockingQueueImpl
import pools.ThreadPool
import kotlin.random.Random
import kotlin.random.nextLong


fun main(args: Array<String>) {
    require(args.size == 1) { "Wrong number of argument, expected 1" }

    val threadCount = args.first().toUIntOrNull()

    requireNotNull(threadCount) {
        "Wrong format, expected: `<NUM_OF_THREADS>`, where <NUM_OF_THREADS> is natural"
    }


    ThreadPool.with(threadCount, BlockingQueueImpl()).use { pool ->
        repeat(times = 50) {
            pool.execute {
                Thread.sleep(Random.nextLong(10L..2000L))
                println("${Thread.currentThread().name}: Hello #$it!")
            }
        }
    }
}
