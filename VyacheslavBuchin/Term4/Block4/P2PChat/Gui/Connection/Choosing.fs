module Gui.Connection.Choosing

open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Layout
open Gui.Connection.State

let view (state: IWritable<State>) =
    StackPanel.create [
        StackPanel.horizontalAlignment HorizontalAlignment.Center
        StackPanel.verticalAlignment VerticalAlignment.Center
        StackPanel.spacing 10
        StackPanel.children [
            Button.create [
                Button.content "Start New"
                Button.onClick (fun _ -> state.Set Creating)
            ]
            Button.create [
                Button.content "Join Existing"
                Button.onClick (fun _ -> state.Set Joining)
            ]
        ]
    ]
