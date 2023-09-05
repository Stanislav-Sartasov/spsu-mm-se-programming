module Dispatcher.NonPriorDispatcherTest

open System
open NUnit.Framework

open Fibers.Dispatcher

[<Test>]
let EmptyCurrentTest () =
    let d = NonPriorDispatcher<int>() :> IDispatcher<int>
    d.Current |> Option.isNone |> Assert.True

[<Test>]
let FIFOTest () =
    let d = NonPriorDispatcher<int>() :> IDispatcher<int>
    let input = List.init 10 id

    for i in input do
        d.Schedule i

    let rec checkEq = function
        | x :: xs ->
            let satisfyCurrent =
                Option.isSome d.Current && d.Current.Value = x
            d.MoveNext()
            satisfyCurrent && checkEq xs
        | [] -> Option.isNone d.Current

    checkEq input |> Assert.True

[<Test>]
let MoveNextOnEmptyTest () =
    let d = NonPriorDispatcher<int>() :> IDispatcher<int>

    Assert.Throws(InvalidOperationException().GetType(), fun () -> d.MoveNext()) |> ignore
