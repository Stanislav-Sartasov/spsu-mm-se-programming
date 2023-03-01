@file:Suppress("NOTHING_TO_INLINE")

package mpi.kotlin

import mpi.Intracomm
import mpi.MPI


internal inline fun Intracomm.gather(
    sendBuf: IntArray,
    sendOffset: Int = 0,
    sendCount: Int = sendBuf.size,
    recvBuf: IntArray,
    recvOffset: Int = 0,
    recvCount: Int = sendBuf.size,
    root: Int = 0,
) = Gather(
    sendBuf, sendOffset, sendCount, MPI.INT,
    recvBuf, recvOffset, recvCount, MPI.INT,
    root
)

internal inline fun Intracomm.gatherV(
    sendBuf: IntArray,
    sendOffset: Int = 0,
    sendCount: Int = sendBuf.size,
    recvBuf: IntArray,
    recvOffset: Int = 0,
    recvCount: IntArray,
    recvDispls: IntArray = recvCount.displacements(),
    root: Int = 0,
) = Gatherv(
    sendBuf, sendOffset, sendCount, MPI.INT,
    recvBuf, recvOffset, recvCount, recvDispls, MPI.INT,
    root
)

internal inline fun Intracomm.bcast(
    buf: IntArray,
    offset: Int = 0,
    count: Int = buf.size,
    root: Int = 0,
) = Bcast(buf, offset, count, MPI.INT, root)

internal inline fun Intracomm.allToAll(
    sendBuf: IntArray,
    sendOffset: Int = 0,
    sendCount: Int,
    recvBuf: IntArray,
    recvOffset: Int = 0,
    recvCount: Int,
) = Alltoall(
    sendBuf, sendOffset, sendCount, MPI.INT,
    recvBuf, recvOffset, recvCount, MPI.INT
)

internal inline fun Intracomm.allToAllV(
    sendBuf: IntArray,
    sendOffset: Int = 0,
    sendCount: IntArray,
    sendDispls: IntArray = sendCount.displacements(),
    recvBuf: IntArray,
    recvOffset: Int = 0,
    recvCount: IntArray,
    recvDispls: IntArray = recvCount.displacements(),
) = Alltoallv(
    sendBuf, sendOffset, sendCount, sendDispls, MPI.INT,
    recvBuf, recvOffset, recvCount, recvDispls, MPI.INT
)


private inline fun IntArray.displacements() = scan(initial = 0) { acc, x -> acc + x }.dropLast(n = 1).toIntArray()
