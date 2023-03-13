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
                    // [0] | [1] 2 | [3] 4 5 | [6] 7 8 9 | ... | [36] .. 44 | [45] .. 54
                    // [x] - borders, i in 0..54
                    val prs = processesData.mapTo(sortedSetOf(), ProcessData::priority).toList()
                    val borders = prs.scan(initial = 0u) { acc, x -> acc + x }.dropLast(n = 1)
                    val i = Random.nextUInt(until = prs.sum())
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
