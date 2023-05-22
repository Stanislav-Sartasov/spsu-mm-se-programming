module Network.SocketExtension

open System
open System.Net
open System.Net.Sockets
open System.Threading.Tasks

let tryIo io =
    async {
        try
            do! io
        with
            | :? AggregateException as ex when (
                ex.InnerException :? TaskCanceledException ||
                ex.InnerException :? OperationCanceledException
                ) -> ()
            | :? TaskCanceledException -> ()
            | :? OperationCanceledException -> ()
    }

type internal Socket with
    static member TcpListenerOf (address: IPAddress) port =
        let endPoint = IPEndPoint(address, port)
        let socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        socket.Bind endPoint
        socket

    static member TcpClientOf (address: IPAddress) port token =
        async {
            let socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            do! socket.ConnectAsync(address, port, token).AsTask() |> Async.AwaitTask |> tryIo
            return socket
        }
