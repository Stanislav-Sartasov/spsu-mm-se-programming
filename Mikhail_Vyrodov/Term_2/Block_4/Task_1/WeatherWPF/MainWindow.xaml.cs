using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using WeatherClasses;

namespace WeatherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IList<WeatherCharacterization> sites = new ObservableCollection<WeatherCharacterization>();
        private ResponseReader respReader;
        private StormglassParametersProvider stormglassParamsProvider;
        private TomorrowParametersProvider tomorrowParamsProvider;
        public MainWindow()
        {
            respReader = new ResponseReader();
            TomorrowioWebHelper tomorrowWebHelper = new TomorrowioWebHelper();
            StormglassioWebHelper stormglassWebHelper = new StormglassioWebHelper();
            stormglassParamsProvider = new StormglassParametersProvider(respReader, stormglassWebHelper);
            tomorrowParamsProvider = new TomorrowParametersProvider(respReader, tomorrowWebHelper);
            tomorrowParamsProvider.FillWeatherProperties();
            sites.Add(tomorrowParamsProvider.Weather);
            stormglassParamsProvider.FillWeatherProperties();
            sites.Add(stormglassParamsProvider.Weather);
            InitializeComponent();
            this.Closed += MainWindow_Closed;
            comboBox.ItemsSource = sites;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            stormglassParamsProvider.FillWeatherProperties();
            if (comboBox.SelectedItem.Equals(sites[0]))
            {
                tomorrowParamsProvider.FillWeatherProperties();
                sites[0] = tomorrowParamsProvider.Weather;
            }
            else
            {
                stormglassParamsProvider.FillWeatherProperties();
                sites[1] = stormglassParamsProvider.Weather;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("See you later");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
