namespace Gui

open System

module Program =
    open Avalonia
    open Avalonia.Controls
    open Avalonia.Controls.ApplicationLifetimes
    open Avalonia.FuncUI
    open Avalonia.FuncUI.Hosts
    open Avalonia.Themes.Fluent

    type MainWindow() =
        inherit HostWindow()
        do
            base.Title <- "Yet Another Chat"
            base.Width <- 800.0
            base.Height <- 600.0
            base.MinWidth <- 250
            base.MinHeight <- 250
            base.SizeToContent <- SizeToContent.Manual
            base.Content <- App.view ()


    type App() =
        inherit Application()

        override this.Initialize() =
            this.Styles.Add (FluentTheme(baseUri = null, Mode = FluentThemeMode.Dark))
            this.Styles.Load "avares://Gui/Styles.xaml"

        override this.OnFrameworkInitializationCompleted() =
            match this.ApplicationLifetime with
            | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
                let window = MainWindow()
                window.Closing.Add (fun _ -> App.disposeChat())
                desktopLifetime.MainWindow <- window
            | _ -> ()

    [<EntryPoint>]
    let main (args: string []) =
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)
