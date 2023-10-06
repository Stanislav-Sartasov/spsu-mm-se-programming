package floyd

import mpi.Intracomm
import mpi.MPI

val comm: Intracomm
    get() = MPI.COMM_WORLD

fun isMasterProcess() = comm.Rank() == 0

fun main(args: Array<String>) {

    println(Runtime.getRuntime().maxMemory())
    println(System.getProperty("sun.arch.data.model"))

    MPI.Init(args)


    val (inputFilename, outputFilename) = args.takeLast(n = 2)
    val (dist, size) = readGraph(inputFilename)

    floyd(dist, size)

    write(outputFilename, dist, size)

    MPI.Finalize()
}