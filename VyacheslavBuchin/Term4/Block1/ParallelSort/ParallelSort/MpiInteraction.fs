module MPITask.MpiInteraction

open MPI

open MpiExec

let send<'a> (value : 'a)(dest : int) (comm : Intracommunicator) =
    comm.Send<'a>(value, dest, 0)

let sendArray<'a> (value : 'a[]) dest (comm : Intracommunicator) =
    comm.Send<'a>(value, dest, 0)
let sendArrayMaster<'a> value = sendArray<'a> value master

let receive<'a> source (comm : Intracommunicator) =
    comm.Receive<'a>(source, master)

let receiveMaster comm = receive master comm

let scatterSend<'a> (values : 'a[]) (comm : Intracommunicator) =
    comm.Scatter<'a>(values)

let scatterReceive<'a> (root : int) (comm : Intracommunicator) =
    comm.Scatter<'a>(root)
