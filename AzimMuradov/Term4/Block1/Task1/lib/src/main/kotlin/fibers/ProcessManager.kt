package fibers

import java.io.Closeable


class ProcessManager(private val strategy: (List<ProcessData>) -> Long) : Closeable {

    var isRunning = false
        private set


    private val fibersByProcess = mutableMapOf<Process, Fiber>()
    private val finishedFibers = mutableListOf<Fiber>()

    private var currProcess: Process? = null


    fun addTask(process: Process) {
        val fiber = Fiber {
            try {
                process.run()
            } catch (e: InterruptedException) {
                e.printStackTrace()
            }
        }
        fibersByProcess[process] = fiber
    }

    fun run() {
        if (isRunning || fibersByProcess.isEmpty()) return
        isRunning = true
        switch(isFiberFinished = false)
    }


    internal fun switch(isFiberFinished: Boolean) {
        if (!isRunning) return

        if (isFiberFinished && currProcess != null) {
            println("Fiber ${fibersByProcess[currProcess]!!.id} finished")
            finishedFibers += fibersByProcess.remove(currProcess)!!
        }

        if (fibersByProcess.isEmpty()) {
            println("All tasks done.")
            return
        }

        val processesData = fibersByProcess.map { (pr, fib) ->
            ProcessData(fib.id, pr.priority)
        }

        val prev = currProcess
        val nextId = strategy(processesData)
        currProcess = fibersByProcess.toList().first { (_, fib) -> fib.id == nextId }.first

        println("Fiber ${fibersByProcess[currProcess]?.id} with priority ${currProcess?.priority} is running")

        if (currProcess !== prev) {
            fibersByProcess[currProcess]?.id?.let(Fiber::switch)
        }
    }


    override fun close() {
        fibersByProcess.values.forEach(Fiber::delete)
        finishedFibers.forEach(Fiber::delete)
        fibersByProcess.clear()
        finishedFibers.clear()
        currProcess = null
        isRunning = false
    }
}
