module TaskTests

open System.Collections.Generic
open System.Threading
open NUnit.Framework
open System.Linq

open ProducerConsumer

let producedValue = 1
let retProducedValue () = producedValue

[<Test>]
[<TestCase(1, 1)>]
[<TestCase(5, 5)>]
[<TestCase(10, 10)>]
let BufferContainsProduced prodCnt consCnt =
    let consumed = List<int>()
    let buffer = List<int>()
    let consume value =
        lock consumed (fun () -> value + value |> consumed.Add)
    let producers = Task.Producer retProducedValue |> Array.create prodCnt
    let consumers = Task.Consumer consume |> Array.create consCnt
    let work () =
        use _ = Array.append producers consumers |> Task.startWith buffer
        Thread.Sleep 400

    work ()

    consumed.All (fun x -> x = producedValue + producedValue)
    |> Assert.IsTrue
    buffer.All (fun x -> x = producedValue)
    |> Assert.IsTrue

[<Test>]
[<TestCase(0, 1)>]
[<TestCase(10, 1)>]
[<TestCase(50, 5)>]
[<TestCase(100, 10)>]
let WithNoProducersNothingProduced length consCnt =
    let consumed = List<int>()
    let buffer = List<int>(Array.create length 1)
    let consume value =
        lock consumed (fun () -> value + 1 |> consumed.Add)
    let consumers = Task.Consumer consume |> Array.create consCnt
    let work () =
        use _ = consumers |> Task.startWith buffer
        Thread.Sleep 400

    work ()

    consumed.Count + buffer.Count = length |> Assert.IsTrue

[<Test>]
[<TestCase(0, 1)>]
[<TestCase(10, 1)>]
[<TestCase(50, 5)>]
[<TestCase(100, 10)>]
let WithNoConsumersNothingConsumed length prodCnt =
    let buffer = List<int>(Array.create length producedValue)
    let producers = Task.Producer retProducedValue |> Array.create prodCnt
    let work () =
        use _ = producers |> Task.startWith buffer
        Thread.Sleep 400

    work ()

    buffer.Count >= length |> Assert.IsTrue

[<Test>]
let WithNoWorkersNothingIsHappenedWithBuffer () =
    let buffer = List<int>()
    let work () =
        use _ = [] |> Task.startWith buffer
        Thread.Sleep 400

    work()

    buffer.Count = 0 |> Assert.IsTrue
