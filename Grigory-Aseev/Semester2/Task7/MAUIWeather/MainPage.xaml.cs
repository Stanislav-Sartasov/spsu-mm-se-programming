using UITools;
using Sites;

namespace MAUIWeather
{
    public partial class MainPage : ContentPage
    {
        public SiteData Data { get; private set; }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Data = Preferences.Get("Site", "tomorrowio") switch
            {
                "openweathermap" => new SiteData(typeof(OpenWeatherMap)),
                "stormglassio" => new SiteData(typeof(StormGlassIO)),
                _ => new SiteData(typeof(TomorrowIO))
            };

            BindingContext = Data;
            base.OnAppearing();
        }

        private void UpdateTomorrow(object sender, EventArgs e)
        {
            UpdateSite(typeof(TomorrowIO));
        }

        private void UpdateStormGlass(object sender, EventArgs e)
        {
            UpdateSite(typeof(StormGlassIO));
        }

        private void UpdateOpenWeatherMap(object sender, EventArgs e)
        {
            UpdateSite(typeof(OpenWeatherMap));
        }

        private void UpdateSite(Type type)
        {
            Preferences.Set("Site", type.Name.ToString().ToLower());
            Data.UpdateData(type);
        }

        private void ExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}