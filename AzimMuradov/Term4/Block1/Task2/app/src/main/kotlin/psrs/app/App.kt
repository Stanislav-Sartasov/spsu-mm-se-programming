package psrs.app

import mpi.MPI
import psrs.IntArrayIO
import psrs.IntArrayPsrs


fun main(args: Array<String>) {
    MPI.Init(args)

    require(args.size == 5) { "Wrong number of arguments. Required 5, but ${args.size} is given." }

    val (inputPath, outputPath) = args.takeLast(n = 2)

    val array = IntArrayIO.readFile(inputPath)

    IntArrayPsrs.sort(array)

    if (MPI.COMM_WORLD.Rank() == 0) {
        IntArrayIO.writeFile(outputPath, array)
    }

    MPI.Finalize()
}
