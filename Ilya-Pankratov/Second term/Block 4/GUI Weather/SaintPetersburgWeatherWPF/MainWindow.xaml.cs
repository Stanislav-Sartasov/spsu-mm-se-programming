using Container;
using Forecast;
using SiteInterface;
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

namespace SaintPetersburgWeatherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ISite> sites;
        private StackPanel weatherPanel;
        private List<SiteWeatherForecast> forecasts;
        private CityWeatherForecast currentForecast;
        private WeatherParameter currentParameter;
        private string currentSite;
        private bool existPressedButton;
        private bool updateSites;


        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            forecasts = new List<SiteWeatherForecast>();
            currentForecast = new CityWeatherForecast("Not Stated", "Not Stated", "Not Stated", "Not Stated", "Not Stated");
            currentSite = String.Empty;
            existPressedButton = false;
            currentParameter = WeatherParameter.Current;
            updateSites = true;
        }

        #region SettingsButtons

        private void MenuOpenWeather_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton((MenuItem)sender, openWeatherButton);
        }

        private void MenuTomorrowIO_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton((MenuItem)sender, tomorrowIOButton);
        }

        private void MenuStormGlass_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton((MenuItem)sender, stormGlassButton);
        }

        private void ToggleButton(MenuItem toggeldButton, Button dependButton)
        {
            string siteName = dependButton.Content.ToString();

            if (currentSite == siteName)
            {
                currentSite = String.Empty;
            }

            if (toggeldButton.IsPressed)
            {
                dependButton.Background = new SolidColorBrush(Colors.LightGray);
                toggeldButton.Background = new SolidColorBrush(Colors.White);
                IoCContainer.RemoveSiteFromContainer(ConvertToSitesName(siteName));
            }
            else
            {
                dependButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0X67, 0x3A, 0xB7));
                toggeldButton.Background = new SolidColorBrush(Color.FromRgb(0xff, 0xcc, 0xff));
                IoCContainer.AddSiteToContainer(ConvertToSitesName(siteName));
            }

            UpdatePanel();
            updateSites = true;
        }

        private SitesName ConvertToSitesName(string siteName)
        {
            switch (siteName)
            {
                case "OpenWeather":
                    return SitesName.OpenWeather;
                case "TomorrowIO":
                    return SitesName.TomorrowIO;
                default:
                    return SitesName.StormGlass;
            }
        }

        #endregion

        #region MenuButtons

        private void SiteButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button.Background.ToString() == "#FF673AB7")
            {
                ReleaseButtons();
                PressSiteButton(button);
                currentSite = (string)button.Content;
                existPressedButton = true;
            }
            else if (button.Background.ToString() != "#FFD3D3D3")
            {
                ReleaseSiteButton(button);
                existPressedButton = false;
                currentSite = (string)button.Content;
                currentSite = String.Empty;
            }
            else
            {
                ReleaseButtons();
                currentSite = String.Empty;
            }

            UpdatePanel();
        }

        private void ReleaseSiteButton(Button button)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0X67, 0x3A, 0xB7));
        }

        private void ReleaseButtons()
        {
            if (stormGlassButton.Background.ToString() == "#FF5A3896")
            {
                ReleaseSiteButton(stormGlassButton);
            }

            if (openWeatherButton.Background.ToString() == "#FF5A3896")
            {
                ReleaseSiteButton(openWeatherButton);
            }

            if (tomorrowIOButton.Background.ToString() == "#FF5A3896")
            {
                ReleaseSiteButton(tomorrowIOButton);
            }
        }

        private void PressSiteButton(Button button)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x5A, 0x38, 0x96));
        }

        private void OpenWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            SiteButton_Click(sender, e);
        }

        private void TomorrowIOButton_Click(object sender, RoutedEventArgs e)
        {
            SiteButton_Click(sender, e);
        }

        private void StormGlassButton_Click(object sender, RoutedEventArgs e)
        {
            SiteButton_Click(sender, e);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            forecasts.Clear();

            if (updateSites)
            {
                sites = IoCContainer.GetSites(currentParameter);
            }

            foreach (var site in sites)
            {
                forecasts.Add(site.GetCityWeatherForecast());
            }

            ChangeCurrentForecast();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region WindowLogic

        private void UpdatePanel()
        {
            if (weatherPanel == null)
            {
                weatherPanel = CreateWeatherInfoPanel();
            }

            if (currentSite == string.Empty)
            {
                if (!TitleGrid.Children.Contains(TitleText))
                {
                    TitleGrid.Children.Add(TitleText);
                }

                if (TitleGrid.Children.Contains(weatherPanel))
                {
                    TitleGrid.Children.Remove(weatherPanel);
                }
            }
            else
            {
                if (TitleGrid.Children.Contains(TitleText))
                {
                    TitleGrid.Children.Remove(TitleText);
                }

                if (!TitleGrid.Children.Contains(weatherPanel))
                {
                    TitleGrid.Children.Add(weatherPanel);
                }

                ChangeCurrentForecast();
            }
        }

        private void ChangeCurrentForecast()
        {
            if (forecasts.Count == 0 || forecasts.Count > 0 && !forecasts.Exists(x => x.Source.Replace(" ", "") == currentSite))
            {
                ChangeAllValues("Not updated");
            }
            else if (!forecasts.Exists(x => x.Source.Replace(" ", "") == currentSite))
            {
                ChangeAllValues("Site is turned down");
            }
            else
            {
                var newForecast = forecasts.Find(x => x.Source.Replace(" ", "") == currentSite);

                if (newForecast == null)
                {
                    ChangeAllValues("Unxpected error");
                    return;
                }
                else if (newForecast.Forecast == null)
                {
                    ChangeAllValues(newForecast.ErrorMessage);
                }
                else
                {
                    ChangeAllValues(newForecast.Forecast[0]);
                }
            }
        }

        private void ChangeAllValues(string newValue)
        {
            currentForecast.CelsiusTemperature = currentForecast.FahrenheitTemperature =
                currentForecast.CloudCover = currentForecast.Humidity = currentForecast.WindSpeed =
                    currentForecast.WindDirection = newValue;
        }

        private void ChangeAllValues(CityWeatherForecast forecast)
        {
            currentForecast.CelsiusTemperature = forecast.CelsiusTemperature;
            currentForecast.FahrenheitTemperature = forecast.FahrenheitTemperature;
            currentForecast.CloudCover = forecast.CloudCover;
            currentForecast.Humidity = forecast.Humidity;
            currentForecast.WindDirection = forecast.WindDirection;
            currentForecast.WindSpeed = forecast.WindSpeed;
        }

        #endregion

        #region WeatherInfoPanelCreation

        private StackPanel CreateWeatherInfoPanel()
        {
            var panel = new StackPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            panel.VerticalAlignment = VerticalAlignment.Center;

            // Date label
            var dateTextBox = CreateLabel();
            dateTextBox.Content = DateTime.Today.ToString("d");
            panel.Children.Add(dateTextBox);

            // Celsius Temperature
            var celcTextBox = CreateLabel();
            celcTextBox.Content = "Celsius Temperature:";

            var celcValue = CreateLabel();
            var celcBinding = new Binding();
            celcBinding.Source = currentForecast;
            celcBinding.Path = new PropertyPath("CelsiusTemperature");
            celcBinding.Mode = BindingMode.TwoWay;
            celcValue.SetBinding(Label.ContentProperty, celcBinding);

            panel.Children.Add(GetFilledGrid(celcTextBox, celcValue));

            // Fahrenheit Temperature
            var fahnTextBox = CreateLabel();
            fahnTextBox.Content = "Fahrenheit Temperature:";

            var fahnValue = CreateLabel();
            var fahnBinding = new Binding();
            fahnBinding.Source = currentForecast;
            fahnBinding.Path = new PropertyPath("FahrenheitTemperature");
            fahnBinding.Mode = BindingMode.TwoWay;
            fahnValue.SetBinding(Label.ContentProperty, fahnBinding);

            panel.Children.Add(GetFilledGrid(fahnTextBox, fahnValue));

            // Humidity
            var humTextBox = CreateLabel();
            humTextBox.Content = "Humidity:";

            var humValue = CreateLabel();
            var humBinding = new Binding();
            humBinding.Source = currentForecast;
            humBinding.Path = new PropertyPath("Humidity");
            humBinding.Mode = BindingMode.TwoWay;
            humValue.SetBinding(Label.ContentProperty, humBinding);

            panel.Children.Add(GetFilledGrid(humTextBox, humValue));

            // Cloud Cover
            var cloudTextBox = CreateLabel();
            cloudTextBox.Content = "Cloud Cover:";

            var cloudValue = CreateLabel();
            var cloudBinding = new Binding();
            cloudBinding.Source = currentForecast;
            cloudBinding.Path = new PropertyPath("CloudCover");
            cloudBinding.Mode = BindingMode.TwoWay;
            cloudValue.SetBinding(Label.ContentProperty, cloudBinding);

            panel.Children.Add(GetFilledGrid(cloudTextBox, cloudValue));

            //Wind Speed
            var speedTextBox = CreateLabel();
            speedTextBox.Content = "Wind Speed:";

            var speedValue = CreateLabel();
            var speedBinding = new Binding();
            speedBinding.Source = currentForecast;
            speedBinding.Path = new PropertyPath("WindSpeed");
            speedBinding.Mode = BindingMode.TwoWay;
            speedValue.SetBinding(Label.ContentProperty, speedBinding);

            panel.Children.Add(GetFilledGrid(speedTextBox, speedValue));

            // WinDirection
            var dirTextBox = CreateLabel();
            dirTextBox.Content = "Wind Direction: ";

            var dirValue = CreateLabel();
            var dirdBinding = new Binding();
            dirdBinding.Source = currentForecast;
            dirdBinding.Path = new PropertyPath("WindDirection");
            dirdBinding.Mode = BindingMode.TwoWay;
            dirValue.SetBinding(Label.ContentProperty, dirdBinding);

            panel.Children.Add(GetFilledGrid(dirTextBox, dirValue));

            return panel;
        }

        private Label CreateLabel()
        {
            var result = new Label();
            result.HorizontalContentAlignment = HorizontalAlignment.Left;
            result.FontSize = 24;
            result.HorizontalAlignment = HorizontalAlignment.Left;
            return result;
        }

        private Grid CreateGrid()
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions[0].Width = GridLength.Auto;
            grid.ColumnDefinitions[1].Width = GridLength.Auto;

            return grid;
        }

        private Grid GetFilledGrid(Label firstLabel, Label seconLabel)
        {
            var grid = CreateGrid();
            grid.Children.Add(firstLabel);
            grid.Children.Add(seconLabel);
            Grid.SetColumn(firstLabel, 0);
            Grid.SetColumn(seconLabel, 1);

            return grid;
        }

        #endregion
    }
}
