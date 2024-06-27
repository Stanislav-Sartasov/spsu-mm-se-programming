package fibers

fun main() {
    println("Starting...")

    Strategies.create(strategy = Strategy.UnPrioritised).use { pm ->
        repeat(times = 5) {
            pm.addTask(Process(pm))
        }

        println("Running...")

        pm.run()

        println("Finishing...")
    }

    println("Finished")

    println()

    println("Starting...")

    Strategies.create(strategy = Strategy.Prioritised).use { pm ->
        repeat(times = 5) {
            pm.addTask(Process(pm))
        }

        println("Running...")

        pm.run()

        println("Finishing...")
    }

    println("Finished")
}