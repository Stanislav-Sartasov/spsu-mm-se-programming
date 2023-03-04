package psrs.app

import mpi.MPI
import psrs.IntArrayIO
import psrs.IntArrayPsrs


fun main(args: Array<String>) {
    MPI.Init(args)

    require(args.size == 5) { "Wrong number of arguments. Required 5, but ${args.size} is given." }

    val (inputFilename, outputFilename) = args.takeLast(n = 2)

    val array = IntArrayIO.readFile(inputFilename)

    IntArrayPsrs.sort(array)

    if (MPI.COMM_WORLD.Rank() == 0) {
        IntArrayIO.writeFile(outputFilename, array)
    }

    MPI.Finalize()
}
