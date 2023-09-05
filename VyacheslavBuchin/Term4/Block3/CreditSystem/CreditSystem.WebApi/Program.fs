open System
open ConcurrentSet
open CreditSystem
open CreditSystem.Credit
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

let argumentError () =
    ArgumentException("<type>\nWhere <type> should be lazy of fine-grained") |> raise

let parseArgs args =
    if Array.length args <> 1 then
        argumentError()
    else
        match args[0] with
        | "lazy" -> LazySet() :> ISet<Credit>
        | "fine-grained" -> FineGrainedSet()
        | _ -> argumentError()
        |> CreditSystem :> IExamSystem

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    let app = builder.Build()

    let system = parseArgs args
    app.MapGet("/api/contains", Func<int64, int64, bool>(fun studentId courseId -> system.Contains studentId courseId)) |> ignore
    app.MapGet("/api/count", Func<int>(fun () -> system.Count)) |> ignore
    app.MapPost("/api/add", Action<int64, int64>(fun studentId courseId -> system.Add studentId courseId)) |> ignore
    app.MapDelete("/api/remove", Action<int64, int64>(fun studentId courseId -> system.Remove studentId courseId)) |> ignore

    app.Run()
    0
