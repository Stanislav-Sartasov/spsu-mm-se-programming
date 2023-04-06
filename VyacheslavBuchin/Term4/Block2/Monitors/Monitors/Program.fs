open System
open System.Collections.Generic
open System.Threading
open ProducerConsumer

let producersCnt = 10
let consumersCnt = 10
let workEmulationInterval = 500

[<EntryPoint>]
let main _ =
    let produce () =
        Thread.Sleep workEmulationInterval // work emulation
        1
    let consume _ =
        Thread.Sleep workEmulationInterval // work emulation
        ()
    let producers = Task.Producer produce |> Array.create producersCnt
    let consumers = Task.Consumer consume |> Array.create consumersCnt
    printf $"Starting task with {producersCnt} producers and {consumersCnt} consumers...\n"
    use _ = Array.append producers consumers |> Task.startWith (List<int>())
    printf "Press any key to stop their work.\n"
    Console.ReadKey() |> ignore
    0
