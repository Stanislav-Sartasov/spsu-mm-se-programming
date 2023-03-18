open MPI
open MPITask

[<EntryPoint>]
let main args =
    use env = new Environment(ref args)
    let comm = Communicator.world
    let ( >>= ) = MpiExec.bind

    let initArray () =
        Array.init 1000 id
        |> MpiExec.ToMaster
    let printResult arr =
        for i in arr do
            printfn $"{i}"
        MpiExec.ToMaster ()

    MpiExec.ret comm () >>= MpiExec.doMaster initArray
    |> Parallel.sort comm >>= MpiExec.doMaster printResult
    |> MpiExec.merge
    0
