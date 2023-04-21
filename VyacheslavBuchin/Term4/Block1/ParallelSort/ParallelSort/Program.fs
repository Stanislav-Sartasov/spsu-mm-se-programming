open System.IO
open System.Runtime.InteropServices
open MPI
open MPITask

let helpMsg = "This program sorts integers. Usage: mpiexec.exe -n <processes_count> <executable> <input_file> <output_file>"
let reportError mes = if Communicator.world.Rank = 0 then printfn $"{mes}\n{helpMsg}"

let ensureProcessesCount pCnt =
    let mutable powerOfTwo = 1
    while powerOfTwo < pCnt do
        powerOfTwo <- powerOfTwo * 2
    if powerOfTwo = pCnt then
        Ok ()
    else
        Error $"Invalid processes count {pCnt}. Should be power of two."

let readArray path =
    try
        let lines = File.ReadAllLines path
        if lines.Length = 1 then
            lines[0].Split [|' '|] |> Array.map int |> Ok
        else
            Error $"{path} is invalid file."
    with e -> Error e.Message

let writeArray path arr =
    try
        let line = Array.map string arr |> String.concat " "
        File.WriteAllText (path, line)
        |> Ok
    with e -> Error e.Message

let parseArgs (args : string[]) =
    if args.Length = 1 && args[0] = "--help" then Error ""
    elif args.Length < 2 then Error "Incorrect arguments."
    else Ok (args[0], args[1])

let inputArray (comm : Intracommunicator) path ([<Out>] array : int[] byref)=
    match readArray path with
    | Ok arr ->
        array <- arr
        true
    | Error mes ->
        reportError mes
        false
    |> Array.create comm.Size |> MpiInteraction.scatterSend<bool> comm

let outputArray path arr =
    match writeArray path arr with
    | Ok _ -> ()
    | Error mes -> reportError mes

[<EntryPoint>]
let main args =
    use env = new Environment(ref args)
    let comm = Communicator.world
    let ( >>= ) = MpiExec.bind
    let mutable array : int[] = null

    let configureState args =
        Result.bind
        <| fun () -> parseArgs args
        <| ensureProcessesCount comm.Size

    match configureState args with
    | Ok (inputFile, outputFile) ->
        let isArrayRead () =
            MpiExec.split (comm.Rank = 0) () ()
            >>= MpiExec.doMpi
                    (fun () -> inputArray comm inputFile &array)
                    (fun () -> MpiInteraction.scatterReceive<bool> comm 0)
            |> MpiExec.merge

        if isArrayRead () |> not then -1
        else
            MpiExec.split (comm.Rank = 0) array ()
            |> Parallel.sort comm
            >>= MpiExec.doMaster (outputArray outputFile)
            |> MpiExec.merge
            0
    | Error mes ->
        reportError mes
        -1
