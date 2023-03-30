import mpi.MPI


class Sorter {
    private val procs = MPI.COMM_WORLD.Size()
    private val rank = MPI.COMM_WORLD.Rank()

    private fun takeSamples(array: IntArray): IntArray {
        return if (array.isNotEmpty())
            IntArray(procs) { array[it * (array.size / procs)] }
        else
            IntArray(procs)
    }

    fun sort(array: IntArray): IntArray {
        // Assign blocks to nodes
        val sendCounts =
            IntArray(procs) { array.size / procs + if (it < (array.size % procs)) 1 else 0 }
        val localBuf = IntArray(sendCounts[rank])
        MPI.COMM_WORLD.scatterv(
            sendbuf = array, sendcounts = sendCounts,
            recvbuf = localBuf
        )


        // Sort local block, take samples and send them to root node
        localBuf.sort()
        val localSample = takeSamples(localBuf)
        val samples = IntArray(procs * procs)
        MPI.COMM_WORLD.gather(sendbuf = localSample, recvbuf = samples)
        // Calculate pivots and send them to all nodes
        samples.sort()
        val pivots = takeSamples(samples).drop(1).toIntArray()
        MPI.COMM_WORLD.bcast(pivots)


        // Number of items sent to i-th node
        val aaSendCounts = IntArray(procs) { i ->
            localBuf.count {
                when (i) {
                    0 -> it < pivots.getOrElse(i) { Int.MAX_VALUE }
                    procs - 1 -> it >= pivots[i - 1]
                    else -> (pivots[i - 1] until pivots[i]).contains(it)
                }
            }
        }

        // Number of items to receive from i-th node
        val aaRecvCounts = IntArray(procs)
        MPI.COMM_WORLD.alltoall(
            sendbuf = aaSendCounts, sendcount = 1,
            recvbuf = aaRecvCounts, recvcount = 1
        )
        // Exchange sorted blocks
        val sortedParts = IntArray(aaRecvCounts.sum())
        MPI.COMM_WORLD.alltoallv(
            sendbuf = localBuf, sendcount = aaSendCounts,
            recvbuf = sortedParts, recvcount = aaRecvCounts
        )


        // Merge
        val parts = buildList {
            val displs = aaRecvCounts.displacements()
            for (i in 0 until procs) {
                add(sortedParts.slice(displs[i] until displs[i] + aaRecvCounts[i]).toMutableList())
            }
        }
        for (elem in sortedParts.indices.reversed()) {
            parts.mapIndexed { i, part -> Pair(i, part.lastOrNull() ?: Int.MIN_VALUE) }
                .maxBy { (_, v) -> v }
                .also { (index, value) ->
                    parts[index].removeLast()
                    sortedParts[elem] = value
                }
        }

        val recvCount = IntArray(procs)
        MPI.COMM_WORLD.gather(sendbuf = intArrayOf(sortedParts.size), recvbuf = recvCount)
        MPI.COMM_WORLD.gatherv(sendbuf = sortedParts, recvbuf = array, recvcount = recvCount)

        return array
    }
}
