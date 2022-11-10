using WeatherAPI;

namespace WinFormsApp
{
	public partial class Form1 : Form
	{
		private readonly TomorrowAPI tomorrowAPI;
		private readonly StormGlassAPI stormGlassAPI;

		public Form1()
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
			var task1 = GetWeatherInfoAsync(tomorrowAPI);
			var task2 = GetWeatherInfoAsync(stormGlassAPI);

			tomorrowIoInfoLabel.Text = await task1;
			stormGlassIoInfoLabel.Text = await task2;
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