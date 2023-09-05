module MPITask.MpiInteraction

open MPI

let send<'a> (comm : Intracommunicator) (value : 'a) (dest : int) =
    comm.Send<'a>(value, dest, 0)

let receive<'a> (comm : Intracommunicator) source =
    comm.Receive<'a>(source, 0)

let scatterSend<'a> (comm : Intracommunicator) (values : 'a[]) =
    comm.Scatter<'a>(values)

let scatterReceive<'a> (comm : Intracommunicator) (source : int) =
    comm.Scatter<'a>(source)
