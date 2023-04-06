module ProducerConsumer.Task

open System
open System.Collections.Generic
open System.Threading
open Microsoft.FSharp.Core

type Worker<'T> =
    | Producer of (unit -> 'T)
    | Consumer of ('T -> unit)

type Task<'T>(buffer : List<'T>) =
    [<VolatileField>]
    let mutable buffer = buffer
    [<VolatileField>]
    let mutable isRunning = false
    let doWhileRunning f ()=
        while isRunning do
            f ()
            Thread.Sleep 1
    let pushBuffer value () = buffer.Add value
    let popBuffer () =
        let indLast = buffer.Count - 1
        if indLast >= 0 then
            let removing = buffer[indLast]
            buffer.RemoveAt indLast
            Some removing
        else None
    let produce f () =
        let value = f ()
        pushBuffer value |> lock buffer
    let consume f () : unit =
        lock buffer popBuffer |> Option.iter f

    let workers = List<Worker<'T>>()
    let threads = List<Thread>()

    member x.Add elems = workers.AddRange elems

    member x.Start () =
        isRunning <- true
        for worker in workers do
            let f =
                match worker with
                | Consumer f -> consume f
                | Producer f -> produce f
                |> doWhileRunning
            let thread = Thread f
            threads.Add thread
            thread.Start()

    member x.Stop () =
        isRunning <- false
        for t in threads do t.Join()

    interface IDisposable with
        member x.Dispose () =
            x.Stop ()

let startWith<'T> buffer workers =
    let task = new Task<'T>(buffer)
    task.Add workers
    task.Start ()
    task
