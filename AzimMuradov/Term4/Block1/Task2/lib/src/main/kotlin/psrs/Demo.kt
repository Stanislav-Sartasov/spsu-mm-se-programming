package psrs

import mpi.MPI


class Demo {

    fun demo(args: Array<String>) {
        MPI.Init(args)
        val me = MPI.COMM_WORLD.Rank()
        val size = MPI.COMM_WORLD.Size()
        println("Hello world from <$me> with <$size>")
        MPI.Finalize()
    }
}
