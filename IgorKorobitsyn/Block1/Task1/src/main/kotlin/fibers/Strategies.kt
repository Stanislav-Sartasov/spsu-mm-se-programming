package fibers

import java.lang.Integer.max
import java.security.Key
import kotlin.random.Random
import kotlin.random.nextUInt

enum class Strategy{
    Prioritised,
    UnPrioritised
}

object Strategies {
    fun create(strategy: Strategy): ProcessManager = when (strategy) {
        Strategy.UnPrioritised -> {
            val cache = mutableSetOf<Int>()
            ProcessManager {fibersByProcess ->
                val fiberIDs = fibersByProcess.values.map(Fiber::getId)
                val waiting = (fiberIDs - cache).toMutableSet()

                val ret: Int = if (waiting.isEmpty()){
                    cache.clear()
                    fiberIDs.first()
                } else{
                    waiting.first()
                }

                cache.add(ret)
                ret
            }
        }

        Strategy.Prioritised -> {
            ProcessManager {fibersByProcess ->

                val priority = run {
                    val unsortedPriorities: MutableList<UInt> = mutableListOf()
                    for (fiber in fibersByProcess){
                        unsortedPriorities.add(fiber.key.getPriority())
                    }
                    val priorities = unsortedPriorities.toList().sorted()
                    val borders = priorities.scan(initial = 0u) { acc, x -> acc + x }.dropLast(n = 1)
                    val i = Random.nextUInt(until = priorities.sum())
                    priorities[borders.indexOfLast { i >= it }]

                }

                val processes = fibersByProcess.filter { it.key.getPriority() == priority }
                val processIds: MutableSet<Int> = mutableSetOf()
                for (fiber in processes){
                    processIds.add(fiber.value.getId())
                }

                processIds.toList()[Random.nextInt(processIds.size)]
            }
        }
    }
}