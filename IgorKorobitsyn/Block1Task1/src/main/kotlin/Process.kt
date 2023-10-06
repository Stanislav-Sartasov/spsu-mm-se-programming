package fibers

import kotlin.random.Random
import kotlin.random.nextUInt

class Process(private val pm: ProcessManager)  {


    private val LONG_PAUSE_BOUNDARY = 10000
    private val SHORT_PAUSE_BOUNDARY = 100
    private val WORK_BOUNDARY = 1000
    private val INTERVALS_AMOUNT_BOUNDARY = 10
    private val PRIORITY_LEVELS_NUMBER = 10

    private var workIntervals: ArrayList<Int> = arrayListOf()
    private var pauseIntervals: ArrayList<Int> = arrayListOf()

    private var priority = 0
    private var totalDuration = 0
    private var activeDuration = 0

    fun getWorkIntervals(): List<Int> {
        return workIntervals
    }

    fun getPriority(): Int {
        return priority
    }

    fun setPriority(newPriority: Int) {
        priority = newPriority
    }

    fun getTotalDuration(): Int {
        totalDuration = activeDuration + pauseIntervals.sum()
        return totalDuration
    }

    fun getActiveDuration(): Int {
        activeDuration = workIntervals.sum()
        return activeDuration
    }

    init {
        var random: Random = Random
        var amount: Int = random.nextInt(INTERVALS_AMOUNT_BOUNDARY)

        for (i in 1..amount){
            workIntervals.add(random.nextInt(WORK_BOUNDARY))
            pauseIntervals.add(random.nextInt(
                if (random.nextDouble() > 0.9) {LONG_PAUSE_BOUNDARY}
                else {SHORT_PAUSE_BOUNDARY})
            )
        }

        priority = random.nextInt(PRIORITY_LEVELS_NUMBER)

        println("Process of priority ${priority} was created")
    }

    fun run(){
        for (i in 1..workIntervals.size) {
            Thread.sleep(workIntervals[i].toLong())

            var pauseBeginTime = System.currentTimeMillis()

            do {
                pm.switch(false)
            }while ((System.currentTimeMillis() - pauseBeginTime) < pauseIntervals[i])
        }

        pm.switch(true)
    }
}