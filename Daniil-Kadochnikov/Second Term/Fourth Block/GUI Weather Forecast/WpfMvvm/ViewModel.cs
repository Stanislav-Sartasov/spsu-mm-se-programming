using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WeatherAPI;

namespace WpfMvvm
{
	internal class ViewModel : INotifyPropertyChanged
	{
		private AWeatherAPI tomorrowAPI;
		private AWeatherAPI stormGlassAPI;

		internal ViewModel()
		{
			tomorrowAPI = new TomorrowAPI();
			stormGlassAPI = new StormGlassAPI();
		}

		private string tomorrowIoString;
		public string TomorrowIoString
		{
			get { return tomorrowIoString; }
			set
			{
				tomorrowIoString = value;
				OnPropertyChanged("tomorrowIoString");
			}
		}

		private string stormGlassIoString;
		public string StormGlassIoString
		{
			get { return stormGlassIoString; }
			set
			{
				stormGlassIoString = value;
				OnPropertyChanged("stormGlassIoString");
			}
		}

		internal async void UpdateTomorrowIoInfoAsync()
		{
			TomorrowIoString = "Loading...";
			TomorrowIoString = await UpdateWeatherString(tomorrowAPI);
		}

		internal async void UpdateStormGlassIoInfoAsync()
		{
			StormGlassIoString = "Loading...";
			StormGlassIoString = await UpdateWeatherString(stormGlassAPI);
		}

		internal async void UpdateBothWeatherInfoAsync()
		{
			TomorrowIoString = "Loading...";
			StormGlassIoString = "Loading...";
			TomorrowIoString = await UpdateWeatherString(tomorrowAPI);
			StormGlassIoString = await UpdateWeatherString(stormGlassAPI);
		}

		private async Task<string> UpdateWeatherString(AWeatherAPI weatherAPI)
		{
			string result;
			try
			{
				result = weatherAPI.GetWeatherModelAsync(await weatherAPI.GetDataAsync()).GetString();
			}
			catch (Exception e)
			{
				return e.Message;
			}
			return result;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}