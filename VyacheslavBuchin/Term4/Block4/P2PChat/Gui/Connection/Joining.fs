module Gui.Connection.Joining

open System.Net
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout
open Gui.Connection.State

let view
    (ip: IWritable<string>)
    (isCorrect: IWritable<bool>)
    (username: IWritable<string>)
    (state: IWritable<State>)
    (errorState: IWritable<string>)
    onJoin =
    StackPanel.create [
        StackPanel.verticalAlignment VerticalAlignment.Center
        StackPanel.horizontalAlignment HorizontalAlignment.Center
        StackPanel.spacing 10
        StackPanel.children [
            TextBox.create [
                TextBox.row 1
                TextBox.watermark "Username"
                TextBox.onTextChanged username.Set
            ]
            TextBox.create [
                TextBox.row 1
                TextBox.watermark "Enter chat IP and port"
                TextBox.onTextChanged (fun text -> ip.Set text; isCorrect.Set true)
            ]
            TextBlock.create [
                TextBlock.isVisible (not isCorrect.Current)
                TextBlock.foreground "red"
                TextBlock.text "Incorrect IP!"
            ]
            TextBlock.create [
                TextBlock.foreground "red"
                TextBlock.text errorState.Current
            ]
            Button.create [
                Button.content "Connect"
                Button.onClick (fun _ ->
                        let toEndPoint (ip : string) =
                            let success, result = IPEndPoint.TryParse(ip)
                            if success then Some result else None
                        errorState.Set ""
                        if username.Current <> null && username.Current.Trim() <> "" then
                            match toEndPoint ip.Current with
                            | Some endPoint ->
                                isCorrect.Set true
                                ip.Set ""
                                onJoin username.Current endPoint
                            | None -> isCorrect.Set false
                    )
            ]
            Button.create [
                Button.content "Back"
                Button.onClick (fun _ -> errorState.Set ""; state.Set Choosing)
            ]
        ]
    ]
