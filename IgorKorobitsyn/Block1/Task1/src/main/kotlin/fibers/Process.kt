package fibers

import java.util.ArrayList
import kotlin.random.Random
import kotlin.random.nextUInt
import kotlin.random.nextInt

class Process {
    private val LONG_PAUSE_BOUNDARY: Int = 10000
    private val SHORT_PAUSE_BOUNDARY: Int = 100
    private val WORK_BOUNDARY: Int = 1000
    private val INTERVALS_AMOUNT_BOUNDARY: Int = 10
    private val PRIORITY_LEVELS_NUMBER: UInt = 10u

    private val workIntervals: ArrayList<Int> = ArrayList()
    private val pauseIntervals: ArrayList<Int> = ArrayList()

    private var priority: UInt = Random.nextUInt(range = 1u..PRIORITY_LEVELS_NUMBER)
    private val totalDuration: Int = 0
    private val activeDuration: Int = 0

    fun getWorkIntervals(): ArrayList<Int>{
        return workIntervals
    }

    fun getPriority(): UInt{
        return priority
    }

    fun setPriority(newPriority: UInt){
        priority = newPriority
    }

    fun getTotalDuration(): Int{
        return totalDuration
    }

    fun getActiveDuration(): Int{
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

        priority = Random.nextUInt(PRIORITY_LEVELS_NUMBER)

        println("Process of priority ${priority} was created")
    }

    fun run(){
        synchronized(this){
            for (i in 0 until workIntervals.size){
                Thread.sleep(workIntervals.get(i).toLong())

                val pauseBeginTime: Long = System.currentTimeMillis()
                do {
                    ProcessManager.switch(false)
                }
                    while ((System.currentTimeMillis() - pauseBeginTime) < pauseIntervals.get(i))

                ProcessManager.switch(true)
            }
        }
    }

}