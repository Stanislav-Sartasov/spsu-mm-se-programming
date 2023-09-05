namespace Network

open System
open System.Collections.Concurrent
open System.Collections.Generic
open System.Net
open System.Threading
open SocketExtension

open System.Net.Sockets

type Client<'a>(serialize : 'a -> byte array, token : CancellationToken) =
    let listeners = ConcurrentDictionary<IPEndPoint, Socket>()

    member client.Listeners = listeners.Keys :> seq<IPEndPoint>

    member client.Connect (address : IPAddress) port =
        async {
            if listeners.ContainsKey(IPEndPoint(address, port)) |> not then
                let! listener = Socket.TcpClientOf address port token
                listeners.TryAdd(IPEndPoint(address, port), listener) |> ignore
        } |> tryIo

    member client.SendMessage msg =
        async {
            for listener in listeners.Values do
                do! listener.SendAsync(serialize msg, SocketFlags.None, token).AsTask() |> Async.AwaitTask |> Async.Ignore
        } |> tryIo

    member client.Disconnect (ipEndPoint : IPEndPoint) =
        let found, socket = listeners.TryGetValue ipEndPoint
        if found then
            listeners.Remove ipEndPoint |> ignore
            socket.Shutdown(SocketShutdown.Both)
            socket.Close()

    interface IDisposable with
        member this.Dispose() =
            for socket in listeners.Values do
                socket.Close()
