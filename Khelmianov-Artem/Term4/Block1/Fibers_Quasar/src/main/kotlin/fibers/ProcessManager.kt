package fibers

import co.paralleluniverse.fibers.Fiber
import co.paralleluniverse.fibers.FiberExecutorScheduler
import co.paralleluniverse.fibers.Suspendable
import co.paralleluniverse.kotlin.fiber
import co.paralleluniverse.strands.Strand


class ProcessManager(
    private val scheduler: FiberExecutorScheduler,
    private val strategy: (MutableList<FiberWithPriority>) -> FiberWithPriority
) {
    private val queue = mutableListOf<FiberWithPriority>()
    private var current: FiberWithPriority? = null

    fun submit(task: MyProcess) =
        queue.add(
            FiberWithPriority(
                fiber(start = false, scheduler = scheduler) @Suspendable {
                    task.run()
                }, task.priority
            )
        )

    @Suspendable
    fun run() {
        if (queue.isEmpty() || current != null) return
        while (queue.isNotEmpty() || current != null) {
            if (current != null) {
                checkCurrent()
            }
            if (queue.isEmpty()) {
                println("All done")
                return
            }
            val next = strategy(queue)
            switchTo(next)
        }
    }

    private fun checkCurrent() = when (current!!.fiber.state) {
        Strand.State.WAITING,
        Strand.State.TIMED_WAITING -> {
            queue.add(current!!)
            current = null
        }

        Strand.State.TERMINATED -> {
            current!!.fiber.join()
            current = null
        }

        else -> throw IllegalStateException("Probably unreachable")
//        Strand.State.NEW
//        Strand.State.STARTED
//        Strand.State.RUNNING
    }

    @Suspendable
    private fun switchTo(curr: FiberWithPriority) = when (curr.fiber.state) {
        Strand.State.NEW -> {
            current = curr
            curr.fiber.start()
            Fiber.yield()
        }

        Strand.State.WAITING,
        Strand.State.TIMED_WAITING -> {
            current = curr
            Fiber.yieldAndUnpark(curr.fiber)
        }

        Strand.State.TERMINATED -> {
            current = curr
            checkCurrent()
        }

        else -> throw IllegalStateException("Probably unreachable")
//        Strand.State.STARTED,
//        Strand.State.RUNNING,
    }

    companion object {
        fun fifo(list: MutableList<FiberWithPriority>): FiberWithPriority {
            return list.removeFirst()
        }

        fun withPriority(list: MutableList<FiberWithPriority>): FiberWithPriority {
            val ret = list.removeAt(list.indexOf(list.maxBy { it.prio }))
            list.onEach { it.prio += 1 }
            return ret
        }
    }
}
