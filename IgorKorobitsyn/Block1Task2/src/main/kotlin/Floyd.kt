package floyd

import mpi.MPI

fun floyd(dist: IntArray, size: Int) {

    val numberOfThreads = comm.Size()
    val length = IntArray(numberOfThreads) { size / numberOfThreads }
    length[numberOfThreads - 1] += size % numberOfThreads

    val range = IntArray(numberOfThreads) { size / numberOfThreads }
    range[numberOfThreads - 1] += size % numberOfThreads

    val start = IntArray(numberOfThreads)
    for (i in 1 until numberOfThreads) {
        start[i] = start[i - 1] + range[i - 1]
    }

    //give each thread the matrix

    comm.Bcast(dist, 0, size * size, MPI.INT, 0)

    val kLine = IntArray(size * numberOfThreads)

    for (k in 0 until size) {

        //gather all the variants of k line in thread #0

        comm.Gather(
            dist, size * k, size, MPI.INT,
            kLine, 0,
            size,
            MPI.INT, 0
        )

        // send the correct k line to everyone

        if (isMasterProcess()) {

            for (process in 0 until numberOfThreads) {
                if (k >= start[process] && k < start[process] + range[process]) {
                    for (i in 0 until size) dist[k * size + i] = kLine[process * size + i]
                    comm.Bcast(dist, k * size, size, MPI.INT, 0)
                    break
                }
            }

        } else {
            comm.Bcast(dist, k * size, size, MPI.INT, 0)
        }

        //the algorithm itself

        for (i in start[comm.Rank()] until start[comm.Rank()] + range[comm.Rank()]) {
            for (j in 0 until size) {
                val newDist = dist[i * size + k] + dist[k * size + j]
                if (newDist < dist[i * size + j] && newDist > 0L) dist[i * size + j] = newDist
            }
        }

    }

    // tying it all together

    val matrices = IntArray(numberOfThreads * size * size)

    comm.Gather(
        dist, 0, size * size, MPI.INT,
        matrices, 0,
        size * size,
        MPI.INT, 0
    )

    if (isMasterProcess()) {
        for (process in 0 until numberOfThreads) {
            for (i in start[process] until start[process] + range[process]) {
                for (j in 0 until size) {
                    dist[i * size + j] = matrices[process * size * size + i * size + j]
                }
            }
        }
    }

}