
open System
open System.Threading
open ThreadPool

let taskCount = 20
let threadCount = 4

let task () =
    let mes = $"Hello world, I'm Thread {Thread.CurrentThread.ManagedThreadId}\n"
    printf $"{mes}"
    Thread.Sleep(1) // work emulation

[<EntryPoint>]
let main _ =
    let tasks = Array.create taskCount task
    printf $"Starting thread pool with {taskCount} tasks on {threadCount} threads\n"
    let pool = new ThreadPool(threadCount, tasks)
    (pool :> IDisposable).Dispose()
    printf "All tasks have been processed\n"
    0
