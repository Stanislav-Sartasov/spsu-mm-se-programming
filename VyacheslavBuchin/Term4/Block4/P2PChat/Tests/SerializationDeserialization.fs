module Tests.UnitTests.SerializationDeserialization

open System.Collections.Generic
open System.IO
open Chat
open Chat.Message
open Foq
open NUnit.Framework
open Network

type private MockTestNetworkStreamReader(bytes : byte array) =
    let stream = new MemoryStream(bytes, false)
    interface INetworkStreamReader with
        member this.ReadExactlyAsync n =
            async {
                let result = Array.zeroCreate<byte> n
                stream.Read(result, 0, n) |> ignore
                return result
            }

[<Test>]
let ``serialization and deserialization should not change the object`` () =
    let name = "BeautifulUsername"
    let ipInfo1 = ("0.0.0.0", 1111)
    let ipInfo2 = ("42.42.42.42", 1337)
    let cases = [|
        Text(name, "Hello World!")
        Members([| ipInfo1; ipInfo2 |])
        RequestMembers ipInfo2
        RequestConnect(name, ipInfo2)
        Disconnect(name, ipInfo2)
    |]
    for case in cases do // used this because of CLI's restriction regarding kinds of attribute parameters
        let serialized = serializeMessage case
        let stream = MockTestNetworkStreamReader(serialized)

        let deserialized = deserializeMessage stream |> Async.RunSynchronously

        Assert.AreEqual(case, deserialized)
