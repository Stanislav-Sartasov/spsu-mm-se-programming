package fibers

import co.paralleluniverse.fibers.Fiber
import co.paralleluniverse.fibers.Suspendable
import co.paralleluniverse.strands.SuspendableRunnable
import co.paralleluniverse.strands.concurrent.ReentrantLock
import fromStart
import time
import java.util.*
import kotlin.concurrent.withLock

class MyProcess(val id: Int = 0) : SuspendableRunnable {
    private val random = Random()
    val priority = random.nextInt(priorityLevelsNumber)
    val intervals = List(random.nextInt(intervalsAmountBoundary)) {
        Pair(
            random.nextInt(workBoundary),
            random.nextInt(if (random.nextDouble() > 0.9) longPauseBoundary else shortPauseBoundary)
        )
    }

    @Suspendable
    override fun run() {
//        synchronized(this) {  // quasar fails to instrument
        ReentrantLock().withLock {
            println("[${fromStart()}] $id starting")
            for ((work, pause) in intervals) {
                println("[${fromStart()}] $id working for $work")
//                Thread.sleep(work.toLong())   // quasar fails to instrument
//                Fiber.sleep(work.toLong())    // yields
                val workBeginTime = time()
                while (time() - workBeginTime < work) continue
                val pauseBeginTime = time()
                println("[${fromStart()}] $id sleeping for $pause")
                do {
                    Fiber.yield()
                } while (time() - pauseBeginTime < pause)
            }
            println("[${fromStart()}] $id end")
        }
    }

    companion object {
        private const val longPauseBoundary = 5000
        private const val shortPauseBoundary = 1000
        private const val workBoundary = 1000
        private const val intervalsAmountBoundary = 5 //10
        private const val priorityLevelsNumber = 10
    }
}