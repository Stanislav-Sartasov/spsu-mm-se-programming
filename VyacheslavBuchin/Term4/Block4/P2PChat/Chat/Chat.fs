namespace Chat

open System
open System.Net
open System.Net.Sockets
open System.Threading
open Message

open Network

type Chat (username,  notify : Notification -> Async<unit>) =
    let tokenSource = new CancellationTokenSource()
    let token = tokenSource.Token
    let mutable isConnected = false

    // some routing hack, works only if you are connected to the network (not only the Internet)
    let myAddress, myPort =
        use socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        socket.Connect("8.8.8.8", 65530)
        let endPoint = socket.LocalEndPoint :?> IPEndPoint
        endPoint.Address, endPoint.Port

    let client = new Client<Message>(serializeMessage, token)
    let onReceive (data : INetworkStreamReader) =
        async {
            let! mes = deserializeMessage data
            match mes with
            | Text (username, msg) ->
                do! Notification.Message (username, msg) |> notify
            | RequestConnect (username, (ip, port)) ->
                let ip = IPAddress.Parse ip
                do! client.Connect ip port
                do! Notification.Connect username |> notify
            | Members ips ->
                if not isConnected then
                    for ip, prt in ips do
                        if ip <> myAddress.ToString() && prt <> myPort then
                            let ip = IPAddress.Parse ip
                            do! client.Connect ip prt
                    do! RequestConnect (username, (myAddress.ToString(), myPort)) |> client.SendMessage
                    isConnected <- true
            | RequestMembers (ip, port) ->
                let address = IPAddress.Parse ip
                do! client.Connect address port
                do!
                    client.Listeners
                    |> Seq.map (fun ipEndPoint -> ipEndPoint.Address.ToString(), ipEndPoint.Port)
                    |> Seq.toArray
                    |> Members
                    |> client.SendMessage
            | Disconnect (username, (ip, port)) ->
                IPEndPoint(IPAddress.Parse ip, port) |> client.Disconnect
                do! Notification.Disconnect username |> notify
        }
    let server = Listener(myAddress, myPort, 10, token, onReceive)

    member val Username = username
    member val Address = myAddress
    member val Port = myPort

    member chat.Run (inviteAddress, invitePort) =
        async {
            server.Run()
            do! client.Connect inviteAddress invitePort
            do!
                RequestMembers(myAddress.ToString(), myPort)
                |> client.SendMessage
        }

    member chat.Run () = server.Run()

    member chat.SendMessage mes =
        Text (username, mes) |> client.SendMessage

    member chat.Disconnect () =
        Disconnect (username, (myAddress.ToString(), myPort)) |> client.SendMessage |> Async.RunSynchronously
        tokenSource.Cancel()
        (client :> IDisposable).Dispose()

    interface IDisposable with
        member chat.Dispose() =
            chat.Disconnect()
