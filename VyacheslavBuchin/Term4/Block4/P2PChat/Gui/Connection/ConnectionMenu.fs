module Gui.Connection.ConnectionMenu

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Gui.Connection
open Avalonia.Layout
open Gui.Connection.State

let private connectionView
    (ip: IWritable<string>)
    (isCorrect: IWritable<bool>)
    (username: IWritable<string>)
    (state : IWritable<State>)
    (errorState: IWritable<string>)
    onStart onJoin =
    match state.Current with
    | Choosing -> Choosing.view state
    | Joining -> Joining.view ip isCorrect username state errorState onJoin
    | Creating -> Creating.view username state errorState onStart
    | Chatting -> failwith "unreachable"

let view
    (ip: IWritable<string>)
    (isCorrect: IWritable<bool>)
    (username: IWritable<string>)
    (state : IWritable<State>)
    (errorState: IWritable<string>)
    onStart onJoin =
    DockPanel.create [
        DockPanel.horizontalAlignment HorizontalAlignment.Center
        DockPanel.children [
            TextBlock.create [
                TextBlock.text "Enter a chat!"
                TextBlock.classes ["title"]
                TextBlock.horizontalAlignment HorizontalAlignment.Center
                TextBlock.dock Dock.Top
            ]
            connectionView ip isCorrect username state errorState onStart onJoin
        ]
    ]
