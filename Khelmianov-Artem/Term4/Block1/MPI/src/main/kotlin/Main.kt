import mpi.MPI


fun main(args: Array<String>) {
    MPI.Init(args)
    val rank = MPI.COMM_WORLD.Rank()
    val (input, output) = args.slice(3..4)
    val array: IntArray
    val len = IntArray(1)

    if (rank == 0) {
        array = readArray(input)
        len[0] = array.size
        MPI.COMM_WORLD.bcast(len)
        MPI.COMM_WORLD.bcast(array)
    } else {
        MPI.COMM_WORLD.bcast(len)
        array = IntArray(len[0])
        MPI.COMM_WORLD.bcast(array)
    }

    Sorter().sort(array)

    if (rank == 0) {
        writeArray(output, array)
    }

    MPI.Finalize()
}
