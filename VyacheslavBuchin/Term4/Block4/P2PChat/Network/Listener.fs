namespace Network

open System
open System.IO
open System.Threading
open System.Threading.Tasks
open SocketExtension

open System.Net
open System.Net.Sockets


type INetworkStreamReader =
    abstract ReadExactlyAsync : int -> Async<byte array>

type NetworkStreamReader(socket: Socket, token : CancellationToken) =
    interface INetworkStreamReader with
        member this.ReadExactlyAsync n =
            async {
                let buffer = Array.zeroCreate<byte> n
                let mutable alreadyReadCount = 0
                while alreadyReadCount <> n do
                    let! count =
                        let buffer = Memory<byte>(buffer, alreadyReadCount, n - alreadyReadCount)
                        socket.ReceiveAsync(buffer, SocketFlags.None, token).AsTask()
                        |> Async.AwaitTask
                    alreadyReadCount <- count + alreadyReadCount
                return buffer
            }

type Listener (address, port, maxConnectionQueueSize, token : CancellationToken, processClient: INetworkStreamReader -> Async<unit>) =
    member listener.Run(): unit =
        let thread =
            (fun () ->
                async {
                    use socket = Socket.TcpListenerOf address port
                    socket.Listen(maxConnectionQueueSize)
                    while true do
                        let! client = socket.AcceptAsync(token).AsTask() |> Async.AwaitTask
                        let processClient () =
                            async {
                                let stream = NetworkStreamReader(client, token)
                                while not token.IsCancellationRequested do
                                    do! processClient stream
                            }
                            |> tryIo
                            |> Async.RunSynchronously
                        Thread(processClient).Start()
                } |> tryIo |> Async.RunSynchronously
            ) |> Thread
        thread.Start()
