namespace CreditSystem

open ConcurrentSet
open CreditSystem.Credit

type CreditSystem(set: ISet<Credit>) =
    let credit s c = { student = s; course = c }

    interface IExamSystem with
        member this.Add student course = credit student course |> set.Add |> ignore
        member this.Contains student course = credit student course |> set.Contains
        member this.Count = set.Count()
        member this.Remove student course = credit student course |> set.Remove |> ignore
