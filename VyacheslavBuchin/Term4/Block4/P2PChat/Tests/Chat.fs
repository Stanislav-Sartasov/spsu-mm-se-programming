module Tests.IntegrationTests.Chat

open System
open Chat
open NUnit.Framework


let private notifyVoidMock _ = async { return () }

[<Test>]
let ``creating and disposing test`` () =
    let chat = new Chat("name1", notifyVoidMock)
    (chat :> IDisposable).Dispose()

[<Test>]
let ``starting new chat and then disconnecting`` () =
    let chat = new Chat("name1", notifyVoidMock)
    chat.Disconnect()
