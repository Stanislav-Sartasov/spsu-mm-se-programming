using System;
using System.Windows;
using System.Windows.Input;
using P2PChat.UI.WPF.MVVM.ViewModel;

namespace P2PChat.UI.WPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void BorderMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
			DragMove();
	}

	private void MinimizeApp(object sender, RoutedEventArgs e)
	{
		if (Application.Current.MainWindow is null) return;

		Application.Current.MainWindow.WindowState = WindowState.Minimized;
	}

	private void ChangeWindowSize(object sender, RoutedEventArgs e)
	{
		if (Application.Current.MainWindow is null) return;

		Application.Current.MainWindow.WindowState =
			Application.Current.MainWindow.WindowState != WindowState.Maximized
				? WindowState.Maximized
				: WindowState.Normal;
	}

	private void ExitApp(object sender, RoutedEventArgs e)
	{
		Application.Current.Shutdown();
		Environment.Exit(0);
	}

	private void OnClose(object? sender, EventArgs e)
	{
		var mainViewModel = DataContext as MainViewModel;
		mainViewModel?.DisposeChats.Execute(sender);
	}
}