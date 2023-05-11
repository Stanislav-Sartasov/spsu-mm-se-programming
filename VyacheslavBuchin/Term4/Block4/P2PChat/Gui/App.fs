module Gui.App

open System
open System.Net
open Avalonia.Controls
open Avalonia.FuncUI
open Chat
open Gui.Connection
open Avalonia.FuncUI.DSL
open Gui.Connection.State
open Gui.ChatTab

let private notify (messages: IWritable<Notification list>) notification =
    async {
        notification :: messages.Current |> messages.Set
    }

let mutable private chat = Unchecked.defaultof<Chat>
let mutable private isChatStarted = false
let internal disposeChat () =
    if isChatStarted then
        (chat :> IDisposable).Dispose()

let private onSending (messages: IWritable<Notification list>) username message =
    Message(username, message) :: messages.Current |> messages.Set
    chat.SendMessage(message) |> Async.Start

let private initChat (address: IWritable<string>) (port: IWritable<int>) (errorDest: IWritable<string>) username notify =
    try
        let newChat = new Chat(username, notify)
        newChat.Address.ToString() |> address.Set
        newChat.Port |> port.Set
        isChatStarted <- true
        Some newChat
    with | _ -> errorDest.Set "Cannot start chat. Check your network."; None

let private onStart (address: IWritable<string>) (port: IWritable<int>) (messages: IWritable<Notification list>) (state: IWritable<State>) (errorDest: IWritable<string>) username =
    match notify messages |> initChat address port errorDest username with
    | Some newChat ->
        newChat.Run()
        chat <- newChat
        Chatting |> state.Set
    | None -> ()

let private onJoin (address: IWritable<string>) (port: IWritable<int>) (messages: IWritable<Notification list>) (state: IWritable<State>) (errorDest: IWritable<string>) username (endPoint: IPEndPoint) =
    match notify messages |> initChat address port errorDest username with
    | Some newChat ->
        newChat.Run(endPoint.Address, endPoint.Port) |> Async.Start
        chat <- newChat
        Chatting |> state.Set
    | None -> ()

let private chatView ip isCorrect username (state: IWritable<State>) address port messages input errorState onStart onJoin onSending =
    match state.Current with
    | Joining | Choosing | Creating ->
        TabItem.create [
            TabItem.header "+"
            ConnectionMenu.view ip isCorrect username state errorState onStart onJoin |> TabItem.content
        ]
    | Chatting ->
        TabItem.create [
            TabItem.header "Chat"
            ChatTab.view address port messages username input onSending |> TabItem.content
        ]

let view () =
    Component(fun ctx ->
        let ip = ctx.useState ""
        let username = ctx.useState ""
        let isCorrect = ctx.useState true
        let state = ctx.useState Choosing
        let address = ctx.useState ""
        let port = ctx.useState 0
        let messages = ctx.useState List.empty<Notification>
        let input = ctx.useState ""
        let errorState = ctx.useState ""
        let onStart = onStart address port messages state errorState
        let onJoin = onJoin address port messages state errorState
        DockPanel.create [
            DockPanel.children [
                TabControl.create [
                    TabControl.tabStripPlacement Dock.Top
                    TabControl.viewItems [
                        chatView ip isCorrect username state address port messages input errorState onStart onJoin (onSending messages)
                    ]
                ]
            ]
        ]
    )
