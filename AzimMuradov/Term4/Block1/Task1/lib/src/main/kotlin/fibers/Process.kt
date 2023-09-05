package fibers

import kotlin.random.Random
import kotlin.random.nextUInt


class Process(private val pm: ProcessManager) {

    var priority: UInt = Random.nextUInt(range = 1u..PRIORITY_LEVELS_NUMBER)
        private set

    private val intervals: List<Interval> = List(size = Random.nextInt(INTERVALS_AMOUNT_BOUNDARY)) {
        Random.nextLong(WORK_BOUNDARY) to Random.nextLong(
            if (Random.nextDouble() > 0.9) LONG_PAUSE_BOUNDARY else SHORT_PAUSE_BOUNDARY
        )
    }

    val activeDuration: Long = intervals.sumOf { it.workTime }

    val totalDuration: Long = intervals.sumOf { it.totalTime }


    @Suppress("BlockingMethodInNonBlockingContext")
    fun run() {
        for ((workTime, pauseTime) in intervals) {
            Thread.sleep(workTime) // work emulation
            val pauseBeginTime = System.currentTimeMillis()
            do {
                pm.switch(isFiberFinished = false)
            } while (System.currentTimeMillis() - pauseBeginTime < pauseTime) // I/O emulation
        }
        pm.switch(isFiberFinished = true)
    }


    companion object {

        private const val PRIORITY_LEVELS_NUMBER = 10u

        private const val INTERVALS_AMOUNT_BOUNDARY = 10
        private const val SHORT_PAUSE_BOUNDARY = 100L
        private const val LONG_PAUSE_BOUNDARY = 100L
        private const val WORK_BOUNDARY = 1000L


        private val Interval.workTime: Long get() = first
        private val Interval.pauseTime: Long get() = second
        private val Interval.totalTime: Long get() = workTime + pauseTime
    }
}

private typealias Interval = Pair<Long, Long>
