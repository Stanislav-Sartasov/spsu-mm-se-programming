using Container;
using Forecast;
using SaintPetersburgWeatherWinForms.Output;
using SiteInterface;

namespace SaintPetersburgWeatherWinForms
{
    public partial class MainForm : Form
    {
        private List<ISite> sites;
        private InfoDisplayer displayer;
        private Panel weatherInfo;
        private bool requestSitesFlag;
        private bool buttonPressed;
        private List<SiteWeatherForecast> forecasts;
        private List<Button> sitesButtons;
        private List<CityWeatherForecast> currentForecasts;
        private List<GroupBox> dayForecasts;
        private List<string> conenctedSites;

        public MainForm()
        {
            InitializeComponent();
        }

        #region MainFormLoadEvents

        private void MainForm_Load(object sender, EventArgs e)
        {
            //initizliaing and creating instance of objects
            forecasts = new List<SiteWeatherForecast>();
            displayer = new InfoDisplayer("Not stated", WeatherParameter.Current);
            requestSitesFlag = true;
            buttonPressed = false;

            // connect all sites
            turnOnButton(this.openWeather);
            turnOnButton(this.tomorrowIO);
            turnOnButton(this.stormGlass);
            conenctedSites = new List<string>()
            {
                "OpenWeather", "StormGlass", "TomorrowIO"
            };

            // set default weather interval parameter
            turnOnButton(this.daily);

            // add addSitesButtons
            sitesButtons = new List<Button>()
            {
                openWeatherButton, tomorrowIOButton, stormGlassButton
            };
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            CalculateGroupBoxLocation();
        }

        #endregion

        #region Menu

        private void turnOnButton(ToolStripMenuItem button)
        {
            button.BackColor = Color.DeepSkyBlue;
            button.Checked = true;
        }

        private void turnOffButton(ToolStripMenuItem button)
        {
            button.BackColor = Color.White;
            button.Checked = false;
        }

        private void toggleButton(ToolStripMenuItem button)
        {
            if (button.Checked)
            {
                turnOffButton(button);
                conenctedSites.Remove(button.Text);
                IoCContainer.RemoveSiteFromContainer(ConvertToSitesName(button.Text));
            }
            else
            {
                turnOnButton(button);
                conenctedSites.Add(button.Text);
                IoCContainer.AddSiteToContainer(ConvertToSitesName(button.Text));
            }

            if (displayer.SourceName == button.Text)
            {
                ChangeCurrentForecasts();
            }
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

        private void daily_Click(object sender, EventArgs e)
        {
            if (!this.daily.Checked)
            {
                displayer.Parameter = WeatherParameter.Current;
                turnOnButton(this.daily);
                turnOffButton(this.weekly);
                UpdateWeatherInfoPanel(displayer.SourceName);
                ChangeCurrentForecasts();
            }
        }

        private void weekly_Click(object sender, EventArgs e)
        {
            if (!this.weekly.Checked)
            {
                displayer.Parameter = WeatherParameter.Week;
                turnOnButton(this.weekly);
                turnOffButton(this.daily);
                UpdateWeatherInfoPanel(displayer.SourceName);
                ChangeCurrentForecasts();
            }
        }

        private void openWeather_Click(object sender, EventArgs e)
        {
            toggleButton(this.openWeather);
        }

        private void stormGlass_Click(object sender, EventArgs e)
        {
            toggleButton(this.stormGlass);
        }

        private void tomorrowIO_Click(object sender, EventArgs e)
        {
            toggleButton(this.tomorrowIO);
        }

        #endregion

        #region ButtonPanel

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            forecasts.Clear();

            if (requestSitesFlag)
            {
                sites = IoCContainer.GetSites(displayer.Parameter);
            }

            foreach (var site in sites)
            {
                forecasts.Add(site.GetCityWeatherForecast());
            }

            if (this.Controls.Contains(weatherInfo))
            {
                ChangeCurrentForecasts();
            }
        }
            
        private void ChangeCurrentForecasts()
        {
            var currentForecast = new SiteWeatherForecast(displayer.SourceName, "Unexpected bug");
            var flag = false;

            if (!conenctedSites.Exists(x => x == displayer.SourceName))
            {
                currentForecast = new SiteWeatherForecast(displayer.SourceName, "This site is off");
            }
            else if (forecasts.Exists(x => x.Source == displayer.SourceName && x.ErrorMessage == "Site is down"))
            {
                currentForecast = forecasts.Find(x => x.Source == displayer.SourceName);
            }
            else if (forecasts.Exists(x => x.Source == displayer.SourceName && x.ErrorMessage == "No errors"))
            {
                currentForecast = forecasts.Find(x => x.Source == displayer.SourceName);
                flag = true;
            }
            else if (forecasts.Exists(x => x.Source == displayer.SourceName))
            {
                currentForecast = forecasts.Find(x => x.Source == displayer.SourceName);
            }
            else
            {
                currentForecast = new SiteWeatherForecast("Not Stated", "Not updated");
            }

            if (flag)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i == 0)
                    {
                        var forecast = currentForecast.Forecast[i];

                        currentForecasts[i].CelsiusTemperature = forecast.CelsiusTemperature;
                        currentForecasts[i].FahrenheitTemperature = forecast.FahrenheitTemperature;
                        currentForecasts[i].CloudCover = forecast.CloudCover;
                        currentForecasts[i].Humidity = forecast.Humidity;
                        currentForecasts[i].WindDirection = forecast.WindDirection;
                        currentForecasts[i].WindSpeed = forecast.WindSpeed;
                    }
                    else
                    {
                        currentForecasts[i].CelsiusTemperature = currentForecasts[i].FahrenheitTemperature = currentForecasts[i].CloudCover
                             = currentForecasts[i].Humidity = currentForecasts[i].WindDirection = currentForecasts[i].WindSpeed = "Not updated";
                    }
                }
            }
            else if (currentForecast.Forecast == null)
            {
                
                for (int i = 0; i < 7; i++)
                {
                    currentForecasts[i].CelsiusTemperature = currentForecasts[i].FahrenheitTemperature = currentForecasts[i].CloudCover
                        = currentForecasts[i].Humidity = currentForecasts[i].WindDirection = currentForecasts[i].WindSpeed = currentForecast.ErrorMessage;
                }

                return;
            }

            for (int i = 0; i < currentForecast.Forecast.Count; i++)
            {
                var forecast = currentForecast.Forecast[i];

                currentForecasts[i].CelsiusTemperature = forecast.CelsiusTemperature;
                currentForecasts[i].FahrenheitTemperature = forecast.FahrenheitTemperature;
                currentForecasts[i].CloudCover = forecast.CloudCover;
                currentForecasts[i].Humidity = forecast.Humidity;
                currentForecasts[i].WindDirection = forecast.WindDirection;
                currentForecasts[i].WindSpeed = forecast.WindSpeed;
            }
        }

        private void Button_Click(Button button, object sender, EventArgs e)
        {
            if (button.BackColor == Color.FromArgb(207, 232, 245)) // if this button is pressed
            {
                ReleaseButton(button);
                titlePanel.Size = weatherInfo.Size;
                this.Controls.Remove(weatherInfo);
                this.Controls.Add(titlePanel);
                buttonPressed = false;
            }
            else
            {
                if (buttonPressed) // if any of other buttons is pressed
                {
                    ReleaseButtons();
                }
                else
                {
                    this.Controls.Remove(titlePanel);
                    if (weatherInfo != null)
                        weatherInfo.Size = titlePanel.Size;
                }

                PressButton(button);
                UpdateWeatherInfoPanel(button.Text);
                buttonPressed = true;
            }

            ChangeCurrentForecasts();
        }

        private void OpenWeatherButton_Click(object sender, EventArgs e)
        {
            Button_Click(openWeatherButton, sender, e);
        }

        private void StormGlassButton_Click(object sender, EventArgs e)
        {
            Button_Click(stormGlassButton, sender, e);
        }

        private void TomorrowIOButton_Click(object sender, EventArgs e)
        {
            Button_Click(tomorrowIOButton, sender, e);
        }

        private void PressButton(Button button)
        {
            button.BackColor = Color.FromArgb(207, 232, 245);
        }

        private void ReleaseButton(Button button)
        {
            button.BackColor = Color.FromArgb(102, 165, 173);
        }

        private void ReleaseButtons()
        {
            foreach (var button in sitesButtons)
            {
                if (button.BackColor == Color.FromArgb(207, 232, 245))
                {
                    ReleaseButton(button);
                }
            }
        }

        private void UpdateWeatherInfoPanel(string sourceName)
        {
            displayer.SourceName = sourceName;

            if (weatherInfo == null)
            {
                CreateWeatherInfoPanel();
            }

            if (displayer.Parameter == WeatherParameter.Current)
            {
                if (!weatherInfo.Controls.Contains(dayForecasts[0]))
                {
                    CalculateGroupBoxLocation();
                    weatherInfo.Controls.Add(dayForecasts[0]);
                }
                else if (weatherInfo.Controls.Contains(dayForecasts[1]))
                {
                    for (int i = 1; i < 7; i++)
                    {
                        weatherInfo.Controls.Remove(dayForecasts[i]);
                    }

                    CalculateGroupBoxLocation();
                }
            }
            else
            {
                if (!weatherInfo.Controls.Contains(dayForecasts[1]))
                {
                    if (!weatherInfo.Contains(dayForecasts[0]))
                    {
                        weatherInfo.Controls.Add(dayForecasts[0]);
                    }

                    CalculateGroupBoxLocation();
                }
            }

            if (!this.Controls.Contains(weatherInfo))
            {
                this.Controls.Add(weatherInfo);
            }
        }

        private void CalculateGroupBoxLocation()
        {
            if (displayer.Parameter == WeatherParameter.Current && weatherInfo != null)
            {
                dayForecasts[0].Location = new Point((weatherInfo.Size - dayForecasts[0].Size) / 2);
            }
            else if (displayer.Parameter == WeatherParameter.Week && weatherInfo != null)
            {
                var size = weatherInfo.Size - dayForecasts[0].Size;
                dayForecasts[0].Location = new Point(size.Width / 4, size.Height / 3);

                for (int i = 1; i < 7; i++)
                {
                    weatherInfo.Controls.Add(dayForecasts[i]);

                    if (i == 3)
                    {
                        dayForecasts[i].Location = new Point(dayForecasts[0].Location.X - dayForecasts[i].Size.Width / 2, dayForecasts[0].Location.Y + dayForecasts[0].Size.Height + 100);
                        continue;
                    }

                    var point = dayForecasts[i - 1].Location;
                    dayForecasts[i].Location = new Point(point.X + (int)(weatherInfo.Size.Width / 4.5), point.Y);
                }
            }
        }

        #endregion

        #region WeatherPanelCreation

        public void CreateWeatherInfoPanel()
        {
            // initialize weather information panel
            weatherInfo = new Panel();
            weatherInfo.Location = titlePanel.Location;
            weatherInfo.Size = titlePanel.Size;
            weatherInfo.AutoSize = false;
            weatherInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            weatherInfo.BackColor = Color.FromArgb(196, 223, 230);
            weatherInfo.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            weatherInfo.Enabled = false;

            // Creating source name label
            weatherInfo.Controls.Add(CreateNameLabel());

            currentForecasts = CreateEmpetySiteForecast().Forecast;
            dayForecasts = new List<GroupBox>();
            
            var time = DateTime.Today;

            for (int i = 0; i < 7; i++)
            {
                dayForecasts.Add(GetDayGroupBox(currentForecasts[i], time.AddDays(i)));
            }
        }

        public Label CreateNameLabel()
        {
            // initializing the label 
            var nameLabel = new Label();
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point((weatherInfo.Size.Width - nameLabel.Size.Width - 350) / 2, 64);
            nameLabel.Font = new Font(FontFamily.GenericMonospace, 50, FontStyle.Regular);
            nameLabel.Anchor = AnchorStyles.Top;

            // creating source
            var source = new BindingSource();
            source.DataSource = typeof(InfoDisplayer);

            // binding to label
            nameLabel.DataBindings.Add(new Binding("Text", source, "SourceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            source.Add(displayer);

            return nameLabel;
        }

        private SiteWeatherForecast CreateEmpetySiteForecast()
        {
            var weather = new List<CityWeatherForecast>();

            for (int i = 0; i < 7; i++)
            {
                weather.Add(CreateFailCityWeatherForecast("Not Stated"));
            }

            return new SiteWeatherForecast(displayer.SourceName, "Empety forecast", weather);
        }

        private CityWeatherForecast CreateFailCityWeatherForecast(string message)
        {
            return new CityWeatherForecast(message, message, message, message, message);
        }

        #endregion

        #region WeatherGroupBox
        private GroupBox GetDayGroupBox(CityWeatherForecast forecast, DateTime day)
        {
            var groupBox = new GroupBox();
            groupBox.Text = day.ToString("d");
            groupBox.AutoSize = true;
            groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;

            // additional settings
            groupBox.TabIndex = 1;
            groupBox.TabStop = false;
            //
            string[] infoArray = new string[12]
            {
                "Celsius Temperature:", forecast.CelsiusTemperature,
                "Fahrenheit Temperature:", forecast.FahrenheitTemperature,
                "Humidity:", forecast.Humidity,
                "Cloud Cover:", forecast.CloudCover,
                "Wind Speed:", forecast.WindSpeed,
                "Wind Direction: ", forecast.WindDirection
            };

            for (int i = 0; i < 6; i++)
            {
                var firstLabel = new Label();
                firstLabel.AutoSize = true;
                firstLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
                firstLabel.Location = new Point(5, (i + 1) * 15);
                firstLabel.Text = infoArray[i * 2];
                groupBox.Controls.Add(firstLabel);

                var secondLabel = new Label();

                var source = new BindingSource();
                source.DataSource = typeof(CityWeatherForecast);

                // binding source to label
                secondLabel.DataBindings.Add(new Binding("Text", source, infoArray[i * 2].Replace(" ", "").Replace(":", ""), true, DataSourceUpdateMode.OnPropertyChanged));
                source.Add(forecast);

                secondLabel.AutoSize = true;
                secondLabel.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
                secondLabel.Location = new Point(firstLabel.Location.X + firstLabel.Size.Width, firstLabel.Location.Y);
                secondLabel.Text = infoArray[(i + 1) * 2 - 1];
                groupBox.Controls.Add(secondLabel);
            }

            return groupBox;
        }

        #endregion
    }
}