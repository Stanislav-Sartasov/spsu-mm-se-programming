namespace Fibers.Dispatcher

open System.Collections.Generic

type NonPriorDispatcher<'T>() =
    let query = Queue<'T>()

    interface IDispatcher<'T> with
        member this.Schedule p =
            query.Enqueue p

        member this.MoveNext() =
            query.Dequeue() |> ignore

        member this.Current
            with get() =
                let found, current = query.TryPeek()
                if found then Some current
                else None
