module Chat.Message

open System
open Network

let private readNBytes (stream: NetworkStreamReader) n =
    async {
        let! res = stream.ReadExactlyAsync n
        return res
    }

let private serializeInt (n: int) = BitConverter.GetBytes n
let private deserializeInt (stream: NetworkStreamReader) =
    async {
        let! bytes = readNBytes stream 4
        return BitConverter.ToInt32 bytes
    }

let private serializeArray<'a> (elems : 'a array) serialize =
    Array.concat [
        serializeInt elems.Length
        Array.map serialize elems |> Array.concat
    ]

let private deserializeArray<'a> (stream: NetworkStreamReader) deserialize =
    async {
        let! count = deserializeInt stream
        return! Array.init count (fun _ -> deserialize stream) |> Async.Sequential
    }

let private serializeByteArray (bytes: byte array) =
    Array.concat [
        serializeInt bytes.Length
        bytes
    ]
let private deserializeByteArray (stream: NetworkStreamReader) =
    async {
        let! size = deserializeInt stream
        return! readNBytes stream size
    }

let private serializeString (str: string) =
    System.Text.UTF32Encoding.UTF32.GetBytes str |> serializeByteArray

let private deserializeString (stream: NetworkStreamReader) =
    async {
        let! bytes = deserializeByteArray stream
        return System.Text.UTF32Encoding.UTF32.GetString bytes
    }

let private textByte = [| byte 1 |]
let private membersByte = [| byte 2 |]
let private requestMembersByte = [| byte 3 |]
let private connectByte = [| byte 4 |]
let private disconnectByte = [| byte 5 |]

type ipInfo = string * int

let private serializeIpInfo (ip, port) = Array.concat [ serializeString ip; serializeInt port ]
let private deserializeIpInfo (stream : NetworkStreamReader) =
    async {
        let! ip = deserializeString stream
        let! port = deserializeInt stream
        return ip, port
    }

type Message =
    | Text of string * string
    | Members of ipInfo array
    | RequestMembers of ipInfo
    | RequestConnect of string * ipInfo
    | Disconnect of string * ipInfo
let serializeMessage = function
    | Text (username, msg) ->
        Array.concat [
            textByte
            serializeString username
            serializeString msg
        ]
    | Members ips ->
        serializeArray ips serializeIpInfo |> Array.append membersByte
    | RequestMembers info -> serializeIpInfo info |> Array.append requestMembersByte
    | RequestConnect (username, info) ->
        Array.concat [
            connectByte
            serializeString username
            serializeIpInfo info
        ]
    | Disconnect (username, info) ->
        Array.concat [
            disconnectByte
            serializeString username
            serializeIpInfo info
        ]

let deserializeMessage (stream : NetworkStreamReader) =
    async {
        let! result =
            async {
                match! readNBytes stream 1 with
                | t when t = textByte ->
                    let! username = deserializeString stream
                    let! msg = deserializeString stream
                    return (username, msg) |> Text |> Some
                | t when t = membersByte ->
                    let! members = deserializeArray stream deserializeIpInfo
                    return members |> Members |> Some
                | t when t = connectByte ->
                    let! username = deserializeString stream
                    let! ipInfo = deserializeIpInfo stream
                    return (username, ipInfo) |> RequestConnect |> Some
                | t when t = disconnectByte ->
                    let! username = deserializeString stream
                    let! ipInfo = deserializeIpInfo stream
                    return (username, ipInfo) |> Disconnect |> Some
                | t when t = requestMembersByte ->
                    let! ipInfo = deserializeIpInfo stream
                    return ipInfo |> RequestMembers |> Some
                | _ -> return None
            }
        if result.IsNone then
            "Unexpected message type byte" |> ArgumentException |> raise
        return result.Value
    }
