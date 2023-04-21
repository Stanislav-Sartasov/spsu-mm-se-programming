import mpi.Datatype
import mpi.Intracomm
import mpi.MPI


fun IntArray.displacements(): IntArray {
    return this.runningFold(0) { acc, i -> acc + i }.dropLast(1).toIntArray()
}

fun Intracomm.alltoall(
    sendbuf: IntArray,
    sendoffset: Int = 0,
    sendcount: Int,
    sendtype: Datatype = MPI.INT,
    recvbuf: IntArray,
    recvoffset: Int = 0,
    recvcount: Int,
    recvtype: Datatype = MPI.INT
) = Alltoall(
    sendbuf, sendoffset, sendcount, sendtype,
    recvbuf, recvoffset, recvcount, recvtype
)

fun Intracomm.alltoallv(
    sendbuf: IntArray,
    sendoffset: Int = 0,
    sendcount: IntArray,
    sdispls: IntArray = sendcount.displacements(),
    sendtype: Datatype = MPI.INT,
    recvbuf: IntArray,
    recvoffset: Int = 0,
    recvcount: IntArray,
    rdispls: IntArray = recvcount.displacements(),
    recvtype: Datatype = MPI.INT
) = Alltoallv(
    sendbuf, sendoffset, sendcount, sdispls, sendtype,
    recvbuf, recvoffset, recvcount, rdispls, recvtype
)


fun Intracomm.bcast(
    buf: IntArray,
    offset: Int = 0,
    count: Int = buf.size,
    type: Datatype = MPI.INT,
    root: Int = 0
) = Bcast(buf, offset, count, type, root)


fun Intracomm.gather(
    sendbuf: IntArray,
    sendoffset: Int = 0,
    sendcounts: Int = sendbuf.size,
    sendtype: Datatype = MPI.INT,
    recvbuf: IntArray,
    recvoffset: Int = 0,
    recvcount: Int = sendbuf.size,
    recvtype: Datatype = MPI.INT,
    root: Int = 0
) = Gather(
    sendbuf, sendoffset, sendcounts, sendtype,
    recvbuf, recvoffset, recvcount, recvtype,
    root
)

fun Intracomm.gatherv(
    sendbuf: IntArray,
    sendoffset: Int = 0,
    sendcounts: Int = sendbuf.size,
    sendtype: Datatype = MPI.INT,
    recvbuf: IntArray,
    recvoffset: Int = 0,
    recvcount: IntArray,
    displs: IntArray = recvcount.displacements(),
    recvtype: Datatype = MPI.INT,
    root: Int = 0
) = Gatherv(
    sendbuf, sendoffset, sendcounts, sendtype,
    recvbuf, recvoffset, recvcount, displs, recvtype,
    root
)

fun Intracomm.scatterv(
    sendbuf: IntArray,
    sendoffset: Int = 0,
    sendcounts: IntArray,
    displs: IntArray = sendcounts.displacements(),
    sendtype: Datatype = MPI.INT,
    recvbuf: IntArray,
    recvoffset: Int = 0,
    recvcount: Int = recvbuf.size,
    recvtype: Datatype = MPI.INT,
    root: Int = 0
) = Scatterv(
    sendbuf, sendoffset, sendcounts, displs, sendtype,
    recvbuf, recvoffset, recvcount, recvtype,
    root
)

