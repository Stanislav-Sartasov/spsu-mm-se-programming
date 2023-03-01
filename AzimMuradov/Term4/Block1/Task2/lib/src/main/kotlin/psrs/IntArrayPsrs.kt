package psrs

import mpi.MPI
import mpi.kotlin.allToAll
import mpi.kotlin.allToAllV
import mpi.kotlin.bcast
import mpi.kotlin.gather
import mpi.kotlin.gatherV


/**
 * Sorter that uses the Parallel Sorting by Regular Sampling (PSRS) algorithm.
 *
 * The sorter uses MPJ Express, so the user need to init and finalize [MPI.COMM_WORLD] outside of the function call.
 */
object IntArrayPsrs : IntArraySorter {

    override fun sort(array: IntArray) {

        // Define processes names

        val i = MPI.COMM_WORLD.Rank()
        val p = MPI.COMM_WORLD.Size()
        val ps = 0 until p
        val root = 0
        val support = 1 until p


        // Step 1 : Separate array in `p` sized parts and sort them sequentially

        val n = array.size
        val start = i * n / p
        val endExclusive = (i + 1) * n / p
        val subArrayIndices = start until endExclusive

        array.sort(fromIndex = start, toIndex = endExclusive)

        val subArray = array.copyOfRange(fromIndex = start, toIndex = endExclusive)


        // Step 2 : Collect pivots

        // m := n / p^2
        // regular samples <-> 0, m, 2 * m, ..., (p - 1) * m

        val regSamples = ps.map { it * n / (p * p) }.map(subArray::get).toIntArray()

        val accRegSamples = IntArray(size = p * p)

        MPI.COMM_WORLD.gather(sendBuf = regSamples, recvBuf = accRegSamples)

        val pivots = if (i == root) {
            val sortedAccRegSamples = accRegSamples
                .asList()
                .chunked(size = p) { it.toMutableList() }
                .merge()

            // pivots <-> p + ⎣p / 2⎦ - 1, 2 * p + ⎣p / 2⎦ - 1, ..., (p - 1) * p + ⎣p / 2⎦ - 1
            support.map { it * p + p / 2 - 1 }.map(sortedAccRegSamples::get).toIntArray()
        } else {
            IntArray(size = p - 1)
        }

        MPI.COMM_WORLD.bcast(pivots)


        // Step 3 : Separate the array into parts using pivots and send them to processes

        val subParts = run {
            val subPartsBorders = IntArray(size = p - 1) { endExclusive }
            val pivotsList = pivots.toMutableList().apply { add(Int.MAX_VALUE); reverse() }

            for ((x, k) in subArray zip subArrayIndices) {
                while (x > pivotsList.last()) {
                    pivotsList.removeLast()
                    subPartsBorders[pivots.size - pivotsList.size] = k
                }
            }

            (listOf(start) + subPartsBorders.asList() + listOf(endExclusive))
                .zipWithNext { st, endEx -> (st until endEx).takeUnless { it.isEmpty() } }
                .map { range -> range?.map(array::get) ?: listOf() }
        }

        val sizeBuf = IntArray(size = p).apply {
            MPI.COMM_WORLD.allToAll(
                sendBuf = subParts.map { it.size }.toIntArray(),
                sendCount = 1,
                recvBuf = this,
                recvCount = 1
            )
        }

        val part = IntArray(size = sizeBuf.sum()).apply {
            MPI.COMM_WORLD.allToAllV(
                sendBuf = subParts.flatten().toIntArray(),
                sendCount = subParts.map { it.size }.toIntArray(),
                recvBuf = this,
                recvCount = sizeBuf
            )
        }

        val sortedPart = sizeBuf
            .scan(initial = 0) { acc, x -> acc + x }
            .zipWithNext()
            .map { (st, endEx) -> part.copyOfRange(st, endEx).toMutableList() }
            .merge()


        // Step 4 : Accumulate sorted parts

        MPI.COMM_WORLD.gather(sendBuf = intArrayOf(sortedPart.size), recvBuf = sizeBuf)

        MPI.COMM_WORLD.gatherV(sendBuf = sortedPart, recvBuf = array, recvCount = sizeBuf)
    }


    private fun List<MutableList<Int>>.merge(): IntArray {
        forEach { it.reverse() }
        return IntArray(size = sumOf(List<Int>::size)) {
            minBy { it.lastOrNull() ?: Int.MAX_VALUE }.removeLastOrNull() ?: Int.MAX_VALUE
        }
    }
}
