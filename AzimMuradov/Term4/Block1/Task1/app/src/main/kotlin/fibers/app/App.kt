package fibers.app

import fibers.Process
import fibers.ProcessManagerFactory
import fibers.ProcessManagerStrategy.RoundRobin


fun main() {
    println("Starting...")

    ProcessManagerFactory.create(strategy = RoundRobin).use { pm ->
        repeat(times = 5) {
            pm.addTask(Process(pm))
        }

        println("Running...")

        pm.run()

        println("Finishing...")
    }

    println("Finished")
}
