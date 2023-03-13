package fibers


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
            val cache = mutableSetOf<Long>()
            ProcessManager { processesData ->
                val max = processesData.maxOf(ProcessData::priority)
                val processesIds = processesData.filter { it.priority == max }.map(ProcessData::id)
                val candidates = processesIds - cache
                if (candidates.isEmpty()) {
                    cache.clear()
                    processesIds.first()
                } else {
                    candidates.first()
                }.also { id -> cache += id }
            }
        }
    }
}
