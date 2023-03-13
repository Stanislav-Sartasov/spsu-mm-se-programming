package fibers

import kotlin.random.Random
import kotlin.random.nextUInt


object ProcessManagerFactory {

    fun create(strategy: ProcessManagerStrategy): ProcessManager = when (strategy) {
        ProcessManagerStrategy.RoundRobin -> {
            val cache = mutableSetOf<Long>()
            ProcessManager { processesData ->
                val processesIds = processesData.map(ProcessData::id)
                val candidates = processesIds - cache
                if (candidates.isEmpty()) {
                    cache.clear()
                    processesIds.first()
                } else {
                    candidates.first()
                }.also { id -> cache += id }
            }
        }

        ProcessManagerStrategy.Prioritized -> {
            val cache = mutableMapOf<UInt, MutableSet<Long>>()
            ProcessManager { processesData ->
                val pr = run {
                    val prs = processesData.mapTo(sortedSetOf(), ProcessData::priority).toList()
                    val borders = prs.scan(initial = 0u) { acc, x -> acc + x }.dropLast(n = 1)
                    val i = Random.nextUInt(until = borders.last())
                    prs[borders.indexOfLast { i >= it }]
                }
                val cacheSegment = cache.getOrDefault(key = pr, defaultValue = mutableSetOf())
                val processesIds = processesData.filter { it.priority == pr }.map(ProcessData::id)
                val candidates = processesIds - cacheSegment
                if (candidates.isEmpty()) {
                    cacheSegment.clear()
                    processesIds.first()
                } else {
                    candidates.first()
                }.also { id -> cacheSegment += id }
            }
        }
    }
}
