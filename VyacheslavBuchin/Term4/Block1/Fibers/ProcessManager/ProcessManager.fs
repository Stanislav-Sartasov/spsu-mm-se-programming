namespace Fibers

open System.Collections.Generic
open System.Threading

open Fibers.Dispatcher

type DispatchingStrategy =
    | Priority
    | NonPriority

type ProcessManager(dispatcher : IDispatcher<processFiber>) =
    let finished = List<uint32>()
    let unprepared = List<Process>()
    let switch () =
        Thread.Sleep(50)
        dispatcher.MoveNext()
        match dispatcher.Current with
        | Some pf -> pf.fiber.Id
        | None -> Fiber.PrimaryId
        |> Fiber.Switch

    static member Init size initializer strategy =
        let dispatcher =
            match strategy with
            | Priority ->
                let getPrior (pf : processFiber) = pf.proc.Priority
                PriorDispatcher<processFiber>(getPrior) :> IDispatcher<processFiber>
            | NonPriority -> NonPriorDispatcher<processFiber>()
        let pm = ProcessManager(dispatcher)
        for i in 0..size-1 do
            initializer i pm |> pm.Add
        pm

    member this.Add p =
        unprepared.Add p

    member this.Execute() =
        let mainFiber = Fiber (fun () ->
            for p in unprepared do
                let toProcessFiber (p : Process) = {
                    proc = p
                    fiber = Fiber (fun () -> p.Run())
                }
                toProcessFiber p |> dispatcher.Schedule

            Fiber.Switch dispatcher.Current.Value.fiber.Id

            for id in finished do
                Fiber.Delete id
        )
        assert mainFiber.IsPrimary
        Fiber.Switch mainFiber.Id

    interface IProcessManager with
        member this.SwitchFinished() =
            assert Option.isSome dispatcher.Current
            let current = dispatcher.Current.Value
            finished.Add current.fiber.Id
            switch()
        member this.Switch() =
            let switch p =
                dispatcher.Schedule p
                switch()
            Option.iter switch dispatcher.Current
