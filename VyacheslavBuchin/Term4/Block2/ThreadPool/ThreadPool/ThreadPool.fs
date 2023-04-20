namespace ThreadPool

open System
open System.Collections.Generic
open System.Threading

type ThreadPool(threadCount, initTasks : (unit -> unit) seq) =
    [<VolatileField>]
    let mutable isRunning = true

    [<VolatileField>]
    let mutable isJoining = false

    let tasks = Queue<unit -> unit>(initTasks)

    let getTask () =
        let haveAnyTask, task = tasks.TryDequeue()
        if haveAnyTask then Some task
        else None

    let initThread _ =
        let threadWork () =
            while isRunning do
                match lock tasks getTask with
                | Some task -> task ()
                | None ->
                    if isJoining then isRunning <- false
        let thread = Thread(threadWork)
        thread.Start()
        thread

    let threads = Array.init threadCount initThread

    new(threadCount) =
         new ThreadPool(threadCount, Seq.empty)

    member x.Join () =
        isJoining <- true
        while isRunning do ()

    member x.Enqueue task =
        lock tasks (fun () -> tasks.Enqueue task)

    interface IDisposable with
        member x.Dispose() =
            isRunning <- false
            for thread in threads do thread.Join()
