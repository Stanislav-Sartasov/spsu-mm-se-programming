package floyd

import mpi.MPI
import java.io.BufferedReader
import java.io.File
import java.io.FileReader

fun readGraph(inputFilename: String): Pair<IntArray, Int> {
    //flattening the 2D array makes it easier to exchange data between threads
    if (isMasterProcess()) {

        val file = File(inputFilename)

        var size = -1

        val dist =
            BufferedReader(FileReader(file)).use { br ->
                var line: String?

                line = br.readLine()
                size = line!!.toInt()
                val dist = IntArray(size * size){ Int.MAX_VALUE}

                while (br.readLine().also { line = it } != null) {
                    val (id1, id2, weight) = line!!.split(' ').map(String::toInt)
                    dist[size * id1 + id2] = weight
                    dist[size * id2 + id1] = weight

                }
                dist
            }

        val sizeForBroadcast = intArrayOf(size)
        comm.Bcast(sizeForBroadcast, 0, 1, MPI.INT, 0)
        size = sizeForBroadcast.first()

//        for (i in 0 until size) {
//            dist[size * i + i] = 0
//        }

// The 5000 example's matrix's diagonal after the algorithm is finished is not all zeros, which was unexpected to me

        return Pair(dist, size)
    } else {
        val sizeForBroadcast = intArrayOf(0)
        comm.Bcast(sizeForBroadcast, 0, 1, MPI.INT, 0)
        val size = sizeForBroadcast.first()
        val dist = IntArray(size * size)
        println(size)
        return Pair(dist, size)
    }
}

fun write(outputFilename: String, distFlat: IntArray, size: Int) {
    if (isMasterProcess()){
        val dist = Array(size) { IntArray(size) }

        for (i in 0 until size) {
            for (j in 0 until size) {
                dist[i][j] = distFlat[i * size + j]
            }
        }

        File(outputFilename).printWriter().use { printer ->
            for (row in dist) {
                printer.println(row.joinToString(separator = " "))
            }
        }
    }
}