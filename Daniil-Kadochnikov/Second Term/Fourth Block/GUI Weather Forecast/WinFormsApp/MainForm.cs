using WeatherAPI;

namespace WinFormsApp
{
	public partial class MainForm : Form
	{
		private readonly TomorrowAPI tomorrowAPI;
		private readonly StormGlassAPI stormGlassAPI;

		public MainForm()
		{
			InitializeComponent();
			tomorrowAPI = new TomorrowAPI();
			stormGlassAPI = new StormGlassAPI();
			GetBothWeatherInfoAsync();
		}

		public void UpdateBothClick(object? sender, EventArgs e)
		{
			tomorrowIoInfoLabel.Text = "Loading...";
			stormGlassIoInfoLabel.Text = "Loading...";
			GetBothWeatherInfoAsync();
		}

		private async void GetBothWeatherInfoAsync()
		{
			var tomorrowIoTask = GetWeatherInfoAsync(tomorrowAPI);
			var stormGlassIoTask = GetWeatherInfoAsync(stormGlassAPI);

			tomorrowIoInfoLabel.Text = await tomorrowIoTask;
			stormGlassIoInfoLabel.Text = await stormGlassIoTask;
		}

		public async void UpdateTomorrowIoClickAsync(object? sender, EventArgs e)
		{
			tomorrowIoInfoLabel.Text = "Loading...";
			tomorrowIoInfoLabel.Text = await GetWeatherInfoAsync(tomorrowAPI);
		}

		private async Task<string> GetWeatherInfoAsync(AWeatherAPI weatherAPI)
		{
			try
			{
				return weatherAPI.GetWeatherModelAsync(await weatherAPI.GetDataAsync()).GetString();
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public async void UpdateStormGlassIoClickAsync(object? sender, EventArgs e)
		{
			stormGlassIoInfoLabel.Text = "Loading...";
			stormGlassIoInfoLabel.Text = await GetWeatherInfoAsync(stormGlassAPI);
		}

		private void ExitButtonClick(object sender, EventArgs e) => Application.Exit();
	}
}