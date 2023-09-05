namespace CreditSystem

type IExamSystem =
    abstract Add : int64 -> int64 -> unit
    abstract Remove : int64 -> int64 -> unit
    abstract Contains : int64 -> int64 -> bool
    abstract Count : int
