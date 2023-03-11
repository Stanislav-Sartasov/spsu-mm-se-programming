
open Fibers

let fibersCount = 5
let strategy = Priority

[<EntryPoint>]
let main _ =
    printf $"Preparing {fibersCount} fibers for execution with strategy: {strategy}\n"
    let pm = ProcessManager.Init fibersCount (fun _ -> Process) strategy
    printf "Execution started"
    pm.Execute()
    printf "Execution finished"
    0
