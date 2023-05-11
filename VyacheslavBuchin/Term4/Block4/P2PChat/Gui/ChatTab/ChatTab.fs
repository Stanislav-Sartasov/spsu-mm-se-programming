module Gui.ChatTab.ChatTab

open Avalonia.Controls.Primitives
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.FuncUI.Types
open Avalonia.Layout
open Avalonia.Media
open Chat

let private textView username text =
     StackPanel.create [
        StackPanel.spacing 3
        StackPanel.margin(0, 5)
        StackPanel.dock Dock.Bottom
        StackPanel.children [
            DockPanel.create [
                DockPanel.children [
                    TextBlock.create [
                        TextBlock.text username
                        TextBlock.dock Dock.Left
                        TextBlock.fontWeight FontWeight.Bold
                    ]
                ]
            ]
            TextBlock.create [
                TextBlock.text text
                TextBlock.textWrapping TextWrapping.WrapWithOverflow
            ]
        ]
    ]

let private notificationView text =
    TextBlock.create [
        TextBlock.text text
        TextBlock.dock Dock.Bottom
        TextBlock.fontWeight FontWeight.Bold
        TextBlock.horizontalAlignment HorizontalAlignment.Center
        TextBlock.padding(0, 10)
    ]

let private messageView : Notification -> IView = function
    | Message(username, text) -> textView username text
    | Connect username -> notificationView $"{username} connected to the chat"
    | Disconnect username -> notificationView $"{username} disconnected from the chat"

let private inputView (username: IWritable<string>) (input: IWritable<string>) onSending =
    Grid.create [
        Grid.dock Dock.Bottom
        Grid.columnDefinitions "*,Auto,Auto"
        Grid.rowDefinitions "Auto,Auto,Auto"
        Grid.children [
            TextBox.create [
                TextBox.onTextChanged (fun t -> if t <> "" then input.Set t)
                TextBox.textWrapping TextWrapping.WrapWithOverflow
                TextBox.horizontalAlignment HorizontalAlignment.Stretch
                TextBox.text input.Current
                TextBox.watermark "Write a message..."
                TextBox.acceptsReturn true
                TextBox.maxHeight 150
                Grid.row 0
                Grid.column 0
            ]
            Button.create [
                Button.minWidth 50
                Button.maxWidth 50
                Button.minHeight 50
                Button.maxHeight 50
                Button.content ">"
                Button.verticalAlignment VerticalAlignment.Bottom
                Grid.row 0
                Grid.column 1
                Button.onClick (fun _ ->
                        let mes = input.Current.Trim()
                        if mes <> "" then
                            input.Set ""
                            onSending username.Current mes
                    )
            ]
        ]
    ]

let private connectionInfoView (address: string) (port: int) =
    DockPanel.create [
        DockPanel.horizontalAlignment HorizontalAlignment.Stretch
        DockPanel.dock Dock.Top
        DockPanel.children [
            TextBlock.create [
                TextBlock.text $"Your IP: {address}:{port}"
                TextBlock.dock Dock.Right
            ]
        ]
    ]

let private emptyMessagesView : IView =
    StackPanel.create [
        StackPanel.dock Dock.Top
        StackPanel.children [ TextBlock.create [] ]
    ]

let private messagesView (msgs: IWritable<Notification list>) =
    DockPanel.create [
        DockPanel.verticalScrollBarVisibility ScrollBarVisibility.Auto
        (List.append <| List.map messageView msgs.Current <| [emptyMessagesView]) |> DockPanel.children
    ]
    |> ScrollViewer.content

let view (address: IWritable<string>) (port: IWritable<int>) (messages: IWritable<Notification list>) username (input: IWritable<string>) onSending =
    DockPanel.create [
        DockPanel.verticalAlignment VerticalAlignment.Stretch
        DockPanel.horizontalAlignment HorizontalAlignment.Stretch
        DockPanel.children [
            connectionInfoView address.Current port.Current
            inputView username input onSending
            ScrollViewer.create [
                ScrollViewer.dock Dock.Bottom
                messagesView messages
            ]
        ]
    ]
