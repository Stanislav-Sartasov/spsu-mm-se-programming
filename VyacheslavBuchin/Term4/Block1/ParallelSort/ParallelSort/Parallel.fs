module MPITask.Parallel

open MPI
open MPITask.MpiExec

let private iterMaster iter p =
    if iter = 0 then 0
    else BitOperation.dropLast iter p

let private ( >>= ) = bind

let pivot (comm : Intracommunicator) iter (arr : MpiResultTarget<int[], int[]>) =
    let p = comm.Rank
    let masterWork arr =
        let pivot =
            let candidates =
                seq {
                    yield Seq.tryHead arr
                    let max = p + (1 <<< iter) - 1
                    for i in p + 1 .. comm.Size do
                        yield MpiInteraction.receive<int option> i comm
                }
                |> Seq.choose id
            Seq.sum candidates / Seq.length candidates
        MpiInteraction.scatterSend<int> (Array.create comm.Size pivot) comm
        |> ToMaster
    let slaveWork arr =
        let master = iterMaster iter p
        let value = Array.tryHead arr
        MpiInteraction.send<int option> value master comm
        MpiInteraction.scatterReceive<int> master comm
        |> ToSlave
    arr >>= doMpi masterWork slaveWork |> merge


let private splitArray (comm : Intracommunicator) (arr : int[]) =
    let pCnt = comm.Size
    let chunks = Array.splitInto pCnt arr
    let chunks =
        if chunks.Length = pCnt then chunks
        else Array.create (pCnt - chunks.Length) [||] |> Array.append chunks
    chunks |> ToMaster

let private sendArrays (comm : Intracommunicator) arrays =
    let masterWork (arrays : int[][]) =
        MpiInteraction.scatterSend arrays comm |> ToMaster
    let slaveWork () =
        MpiInteraction.scatterReceive<int[]> master comm |> ToSlave
    arrays >>= doMpi masterWork slaveWork

let private workingSegment (comm : Intracommunicator) (arr : MpiResultTarget<int[], _>) =
    let split = splitArray comm |> doMaster
    arr >>= split |> sendArrays comm |> merge

let exchange comm arr dest iter sep =
    let left, right = Array.partition (fun x -> x < sep) arr
    if BitOperation.get iter dest = 1 then
        MpiInteraction.sendArray<int> right dest comm
        let right = MpiInteraction.receive<int[]> dest comm
        Array.append left right
    else
        MpiInteraction.sendArray<int> left dest comm
        let left = MpiInteraction.receive<int[]> dest comm
        Array.append left right

let private sortChunks (comm : Intracommunicator) (arr : int[]) =
    let mutable arr = arr
    let pCnt = comm.Size
    let p = comm.Rank
    let iterCnt = BitOperation.significantCnt pCnt

    for i in iterCnt - 1..1 do
        let master = iterMaster i p
        let sep =
            arr
            |> if p = master then ToMaster else ToSlave
            |> pivot comm i
        let pair = BitOperation.invert i p

        if pair < pCnt then arr <- exchange comm arr pair i sep

    Array.sort arr
    |> ret comm

let sort (comm : Intracommunicator) arr =
    let slaveWork arr =
        MpiInteraction.sendArrayMaster<int> arr comm
        |> ToSlave
    let masterWork arr =
        seq {
            yield arr
            for i in 1..comm.Size do
                yield MpiInteraction.receive<int[]> i comm
        }
        |> Array.concat |> ToMaster
    let arr = workingSegment comm arr |> sortChunks comm
    arr >>= doMpi masterWork slaveWork
