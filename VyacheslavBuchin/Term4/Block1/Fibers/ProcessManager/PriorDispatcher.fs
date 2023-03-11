namespace Fibers.Dispatcher

open System
open System.Collections.Generic

type private priority = {
    prior : int32
    lastRun : DateTime
}

type PriorDispatcher<'T>(getPrior : 'T -> int32) =

    let getPrior p = {
        prior = getPrior p
        lastRun = DateTime.Now
    }
    let comparer =
        (fun a b -> a.prior - b.prior + ((a.lastRun - b.lastRun).TotalSeconds |> Convert.ToInt32))
        |> Comparer<priority>.Create
    let query = PriorityQueue<'T, priority>(comparer)

    interface IDispatcher<'T> with
        member this.Current
            with get() =
                let found, current, _ = query.TryPeek()
                if found then Some current
                else None

        member this.Schedule p =
            let prior = getPrior p
            query.Enqueue(p, prior)

        member this.MoveNext() =
            query.Dequeue() |> ignore
