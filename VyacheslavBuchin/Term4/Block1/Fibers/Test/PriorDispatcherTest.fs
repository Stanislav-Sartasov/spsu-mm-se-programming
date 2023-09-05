module Dispatcher.PriorDispatcherTest

open System
open System.Threading
open NUnit.Framework

open Fibers.Dispatcher

[<Test>]
let EmptyCurrentTest () =
    let d = PriorDispatcher<int>(id) :> IDispatcher<int>
    d.Current |> Option.isNone |> Assert.True

[<Test>]
let AgingTest () =
    let d = PriorDispatcher<int>(fun _ -> 5) :> IDispatcher<int>

    d.Schedule 0
    d.Schedule 1
    Thread.Sleep(1500) // work emulation
    let first = d.Current.Value
    d.MoveNext()
    d.Schedule first

    let second = d.Current.Value
    Assert.AreNotEqual (second, first)

[<Test>]
let MoveNextOnEmptyTest () =
    let d = PriorDispatcher<int>(id) :> IDispatcher<int>

    Assert.Throws(InvalidOperationException().GetType(), fun () -> d.MoveNext()) |> ignore
