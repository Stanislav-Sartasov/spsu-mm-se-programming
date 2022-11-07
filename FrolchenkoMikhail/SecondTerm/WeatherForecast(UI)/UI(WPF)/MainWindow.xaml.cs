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
		}

		private void StormglassIoCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			sites.Item2 = true;
		}

		private void StormglassIoCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			sites.Item2 = false;
		}

		private void TomorrowIoCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			sites.Item1 = true;
		}

		private void TomorrowIoCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			sites.Item1 = false;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sites.Item1)
			{
				MessageBox.Show($"Tomorrow.io:\n\n{new TomorrowIo().ShowWeather()}");
			}
			if (sites.Item2)
			{
				MessageBox.Show($"Stomglass.io:\n\n{new StormglassIo().ShowWeather()}");
			}
			if (!(sites.Item1 || sites.Item2))
			{
				MessageBox.Show("Сhoose at least 1 site");
			}
		}
	}
}
