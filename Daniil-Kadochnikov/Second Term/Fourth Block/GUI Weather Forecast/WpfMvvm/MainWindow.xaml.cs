using System.Windows;

namespace WpfMvvm
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();
			viewModel = new ViewModel();
			DataContext = viewModel;
			viewModel.UpdateBothWeatherInfoAsync();
		}

		private void UpdateBothForecasts(object sender, RoutedEventArgs e) => viewModel.UpdateBothWeatherInfoAsync();

		private void TomorrowIoUpdateForecast(object sender, RoutedEventArgs e) => viewModel.UpdateTomorrowIoInfoAsync();

		private void StormGlassIoUpdateForecast(object sender, RoutedEventArgs e) => viewModel.UpdateStormGlassIoInfoAsync();

		private void Exit(object sender, RoutedEventArgs e) => this.Close();
	}
}