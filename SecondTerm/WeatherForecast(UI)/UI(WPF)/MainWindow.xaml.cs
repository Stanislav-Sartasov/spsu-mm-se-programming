using Sites;
using System.Windows;

namespace UI_WPF_
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private (bool, bool) sites;

		public MainWindow()
		{
			InitializeComponent();
			MessageBox.Show("This program output weather forecast\n" +
				"Select a site and click on the \"Refresh\" button to get update information");
		}

		private void StormglassIoCheckBoxChecked(object sender, RoutedEventArgs e)
		{
			sites.Item2 = true;
		}

		private void StormglassIoCheckBoxUnchecked(object sender, RoutedEventArgs e)
		{
			sites.Item2 = false;
		}

		private void TomorrowIoCheckBoxChecked(object sender, RoutedEventArgs e)
		{
			sites.Item1 = true;
		}

		private void TomorrowIoCheckBoxUnchecked(object sender, RoutedEventArgs e)
		{
			sites.Item1 = false;
		}

		private void RefreshButtonClick(object sender, RoutedEventArgs e)
		{
			if (sites.Item1)
			{
				tomorrowIoTextBlock.Text = new TomorrowIo().ShowWeather();
			}
			if (sites.Item2)
			{
				stormglassIoTextBlock.Text = new StormglassIo().ShowWeather();
			}
			if (!(sites.Item1 || sites.Item2))
			{
				MessageBox.Show("Сhoose at least 1 site");
			}
		}

		private void ClearButtonClick(object sender, RoutedEventArgs e)
		{
			if (sites.Item1)
			{
				tomorrowIoTextBlock.Text = "";
			}
			if (sites.Item2)
			{
				stormglassIoTextBlock.Text = "";
			}
		}
	}
}
