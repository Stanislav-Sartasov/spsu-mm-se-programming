namespace ThreadPool

open System
open System.Collections.Generic
open System.Threading
open System.Linq

type ThreadPool(threadCount, initTasks : (unit -> unit) seq) =
    [<VolatileField>]
    let mutable isDisposed = false
    let tasks = Queue<unit -> unit>(initTasks)
    let dequeueSync = obj()

    let getTask () =
        let haveAnyTask, task = tasks.TryDequeue()
        if haveAnyTask then Some task
        else None

    let initThread _ =
        let threadWork () =
            let mutable isRunning = true
            while isRunning do
                lock dequeueSync (fun () ->
                        if not isDisposed && (tasks.Any() |> not) then
                            Monitor.Wait dequeueSync |> ignore
                        if isDisposed && (tasks.Any() |> not) then
                            isRunning <- false
                            None
                        else getTask()
                    )
                |> Option.iter (fun f -> f())
        let thread = Thread(threadWork)
        thread.Start()
        thread

    let threads = Array.init threadCount initThread

    new(threadCount) =
         new ThreadPool(threadCount, Seq.empty)

    member x.Enqueue task =
        let enqueue () =
            tasks.Enqueue task
            Monitor.Pulse dequeueSync
        if isDisposed then raise (InvalidOperationException())
        lock dequeueSync enqueue

    interface IDisposable with
        member x.Dispose() =
            isDisposed <- true
            lock dequeueSync (fun () -> Monitor.PulseAll dequeueSync)
            for thread in threads do thread.Join()
