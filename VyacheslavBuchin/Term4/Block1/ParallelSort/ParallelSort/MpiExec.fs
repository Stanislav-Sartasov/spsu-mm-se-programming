module MPITask.MpiExec

open MPI

let master = 0
let isMaster (comm : Intracommunicator) = comm.Rank = master
let isSlave comm = isMaster comm |> not

type MpiResultTarget<'a, 'b> =
    | ToMaster of 'a
    | ToSlave of 'b

type mpiKArrow<'a, 'b, 'c, 'd> = {
    forMaster : 'a -> MpiResultTarget<'c, 'd>
    forSlave : 'b -> MpiResultTarget<'c, 'd>
}

let ret comm = if isMaster comm then ToMaster else ToSlave
let bind<'a, 'b, 'c, 'd> r (f : mpiKArrow<'a, 'b, 'c, 'd>) =
    match r with
    | ToMaster r -> f.forMaster r
    | ToSlave r -> f.forSlave r
let private ( >>= ) = bind

let fmap masterWork slaveWork a b comm =
    if isMaster comm then
        masterWork a |> ToMaster
    else
        slaveWork b |> ToSlave

let doMpi f g =
    {
        forMaster = f
        forSlave = g
    }
let doMaster f = doMpi f (fun _ -> ToSlave ())

let merge = function
    | ToMaster r
    | ToSlave r -> r
