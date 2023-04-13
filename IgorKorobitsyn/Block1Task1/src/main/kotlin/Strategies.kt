package fibers

import java.lang.Integer.max
import kotlin.random.Random

enum class Strategy{
    Prioritised,
    UnPrioritised
}

class PrioritisedFiber(val id: Int, val priority: Int)

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
            val cache: MutableMap<Int,MutableSet<PrioritisedFiber>> = mutableMapOf()
            ProcessManager {fibersByProcess ->

                val fibers: MutableMap<Int,MutableSet<PrioritisedFiber>> = mutableMapOf()
                var maxPriority: Int = -1

                for (fiber in fibersByProcess){

                    val priority = fiber.key.getPriority()
                    val id = fiber.value.getId()
                    val entry = PrioritisedFiber(id, priority)

                    if (fibers.containsKey(priority)){
                        fibers[priority]!!.add(entry)
                    }
                    else{
                        fibers[priority] = mutableSetOf(entry)
                    }

                    maxPriority = max(maxPriority, priority)
                }

                val highPriorityFibers = fibers.getOrDefault(key = maxPriority, defaultValue = mutableSetOf())
                val highPriorityCache = cache.getOrDefault(key = maxPriority, defaultValue = mutableSetOf())

                val waiting = highPriorityFibers - highPriorityCache

                val choosingFrom: List<PrioritisedFiber> = if (waiting.isEmpty()){
                    highPriorityCache.clear()
                    highPriorityFibers.toList()
                } else{
                    waiting.toList()
                }

                val randomIndex = Random.nextInt(choosingFrom.size)

                //println(choosingFrom)

                highPriorityCache.add(choosingFrom[randomIndex])
                choosingFrom[randomIndex].id
            }
        }
    }
}