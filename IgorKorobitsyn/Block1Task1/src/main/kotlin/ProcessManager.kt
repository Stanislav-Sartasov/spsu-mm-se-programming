package fibers

import java.io.Closeable

class ProcessManager (private val strategy: (MutableMap<Process, Fiber>) -> Int) : Closeable {

    var isRunning = false

    private val fibersByProcess = mutableMapOf<Process, Fiber>()
    private val processesByFiberId = mutableMapOf<Int, Process>()
    private val finishedFibers = mutableListOf<Fiber>()

    private var currentProcess: Process? = null

    fun addTask(newProcess: Process){
        val fiber = Fiber{
            try {
                newProcess.run()
            } catch (e: InterruptedException) {
                e.printStackTrace()
            }
        }
        fibersByProcess[newProcess] = fiber
        processesByFiberId[fiber.getId()] = newProcess
    }

    fun run(){
        if (isRunning || fibersByProcess.isEmpty()) return
        isRunning = true
        switch(false)
    }

    fun switch(isFiberFinished: Boolean) {
        if (!isRunning) return

        if (isFiberFinished && currentProcess != null){
            val finishedFiber: Fiber = fibersByProcess.remove(currentProcess)!!
            finishedFibers += finishedFiber
            println("fiber #${finishedFiber.getId()} is finished")
        }

        if (fibersByProcess.isEmpty()){
            println("all fibers are finished")
            return
        }

        val previousProcess = currentProcess
        val nextId = strategy(fibersByProcess)
        currentProcess = processesByFiberId[nextId]
        val fiber: Fiber = fibersByProcess[currentProcess]!!

        if (currentProcess != previousProcess) run {
            fiber.switch(fiber.getId())
        }

        println("switched to fiber #${fiber.getId()} of priority ${currentProcess!!.getPriority()}")
    }

    override fun close() {

        fibersByProcess.values.forEach { fiber -> fiber.delete() }
        finishedFibers.forEach { fiber -> fiber.delete() }

        isRunning = false
        fibersByProcess.clear()
        processesByFiberId.clear()
        finishedFibers.clear()
        currentProcess = null
    }
}