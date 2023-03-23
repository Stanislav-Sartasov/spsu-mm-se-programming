module MPITask.Parallel

open System
open MPI
open MPITask.MpiExec

type ProcessesGroup(proc: int, iter: int) =
    let master = BitOperation.dropLast (iter + 1) proc
    let firstSlave = master + 1
    let lastSlave = BitOperation.raiseLast (iter + 1) master
    let slaves = seq { firstSlave .. lastSlave } |> Seq.toArray

    member this.Master = master
    member this.isMaster = proc = master
    member this.Slaves = slaves

let private ( >>= ) = bind

let private rnd = Random()

let getPivot (comm: Intracommunicator) (processesGroup : ProcessesGroup) =
    let randomElement arr =
        if Array.isEmpty arr then None
        else
            let index = arr.Length |> rnd.Next
            Some arr[index]

    let masterWork arr =
        assert processesGroup.isMaster
        let pivot =
            seq {
                yield randomElement arr
                yield! Array.map <| MpiInteraction.receive<int option> comm <| processesGroup.Slaves
            }
            |> Seq.choose id
            |> Seq.averageBy float
            |> int

        Array.iter <| MpiInteraction.send<int> comm pivot <| processesGroup.Slaves
        pivot

    let slaveWork arr =
        let value = randomElement arr
        MpiInteraction.send<int option> comm value processesGroup.Master
        MpiInteraction.receive<int> comm processesGroup.Master

    doMpi masterWork slaveWork


let private splitArray (comm: Intracommunicator) (arr: int[]) =
    let pCnt = comm.Size
    let chunks = Array.splitInto pCnt arr
    let chunks =
        if chunks.Length = pCnt then chunks
        else Array.create (pCnt - chunks.Length) [||] |> Array.append chunks
    chunks

let exchange comm arr dest iter pivot =
    let left, right = Array.partition (fun x -> x < pivot) arr

    if BitOperation.get iter dest = 1 then
        MpiInteraction.send<int[]> comm right dest
        let right = MpiInteraction.receive<int[]> comm dest
        Array.append left right
    else
        let left' = MpiInteraction.receive<int[]> comm dest
        MpiInteraction.send<int[]> comm left dest
        Array.append left' right

let private sortChunks (comm: Intracommunicator) (arr: int[]) =
    let mutable arr = arr
    let pCnt = comm.Size
    let p = comm.Rank
    let iterCnt = BitOperation.significantCnt pCnt

    for i in iterCnt - 1 .. -1 .. 0 do
        let pg = ProcessesGroup(p, i)
        let pivot =
            split pg.isMaster arr arr
            >>= getPivot comm pg
            |> merge
        let pair = BitOperation.invert i p
        arr <- exchange comm arr pair i pivot

    Array.sort arr

let sort (comm : Intracommunicator) (arr : MpiResultTarget<int[], unit>) =
    let scatterChunks =
        let send = MpiInteraction.scatterSend<int[]> comm
        let receive () = MpiInteraction.scatterReceive<int[]> comm 0
        doMpi send receive

    let gatherChunks =
        let send arr = MpiInteraction.send<int[]> comm arr 0
        let receive arr =
            seq {
                yield arr
                for i in 1 .. comm.Size - 1 do
                    yield MpiInteraction.receive<int[]> comm i
            }
            |> Array.concat
        doMpi receive send

    arr
    >>= doMaster (splitArray comm)
    >>= scatterChunks
    >>= doAll (sortChunks comm)
    >>= gatherChunks
