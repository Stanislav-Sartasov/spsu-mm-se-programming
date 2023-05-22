module Gui.Connection.Creating

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout
open Gui.Connection.State

let view (username: IWritable<string>) (state: IWritable<State>) (errorState: IWritable<string>) onStart =
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
            TextBlock.create [
                TextBlock.text errorState.Current
                TextBlock.foreground "red"
            ]
            Button.create [
                Button.content "Start chatting!"
                Button.onClick (fun _ ->
                    errorState.Set ""
                    if username.Current <> null && username.Current.Trim() <> "" then
                        onStart username.Current
                )
            ]
            Button.create [
                Button.content "Back"
                Button.onClick (fun _ -> errorState.Set ""; state.Set Choosing)
            ]
        ]
    ]
