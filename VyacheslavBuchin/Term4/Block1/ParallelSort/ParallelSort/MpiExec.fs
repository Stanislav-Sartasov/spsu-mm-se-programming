module MPITask.MpiExec

type MpiResultTarget<'a, 'b> =
    | ToMaster of 'a
    | ToSlave of 'b

type mpiKArrow<'a, 'b, 'c, 'd> = {
    forMaster : 'a -> MpiResultTarget<'b, 'd>
    forSlave : 'c -> MpiResultTarget<'b, 'd>
}

let bind<'a, 'b, 'c, 'd> r (f : mpiKArrow<'a, 'b, 'c, 'd>) =
    match r with
    | ToMaster r -> f.forMaster r
    | ToSlave r -> f.forSlave r

let doMpi f g =
    {
        forMaster = f >> ToMaster
        forSlave = g >> ToSlave
    }
let doAll f = doMpi f f
let doMaster f = doMpi f id

let split flag masterData slaveData =
    if flag then ToMaster masterData
    else ToSlave slaveData

let merge = function
    | ToMaster r
    | ToSlave r -> r
