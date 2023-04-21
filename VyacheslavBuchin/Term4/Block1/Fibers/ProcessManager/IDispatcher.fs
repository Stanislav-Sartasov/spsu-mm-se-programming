namespace Fibers.Dispatcher

open Fibers

type processFiber = {
    proc : Process
    fiber : Fiber
}

type IDispatcher<'T> =
    abstract member MoveNext : unit -> unit
    abstract member Schedule : 'T -> unit
    abstract member Current : 'T option
