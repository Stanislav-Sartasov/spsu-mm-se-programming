using System.Reflection;

namespace AbstractWeatherForecast
{
    public abstract class AWeatherForecast
    {
        protected AParser dataParser;

        public string CurrentCelsiusTemperature { get; protected set; } = "No data";
        public string CurrentFahrenheitTemperature { get; protected set; } = "No data";
        public string CloudCover { get; protected set; } = "No data";
        public string Precipitation { get; protected set; } = "No data";
        public string AirHumidity { get; protected set; } = "No data";
        public string WildDirection { get; protected set; } = "No data";
        public string WildSpeed { get; protected set; } = "No data";

        protected abstract void ShowDescription();

        public AWeatherForecast(AParser dataParser)
        {
            this.dataParser = dataParser;
        }

        public void Update()
        {
            int i = 0;
            try
            {
                List<string> result = dataParser.GetListOfCurrentData();
                foreach (PropertyInfo info in this.GetType().GetProperties())
                {
                    info.SetValue(this, result[i]);
                    i++;
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Data Parser not inisialize!");
            }
        }

        public List<String> ShowFullWeatherForecast()
        {
            List<String> allParams = new List<String>();
            foreach (PropertyInfo info in this.GetType().GetProperties())
            {
                allParams.Add((String)info.GetValue(this));
            }
            return allParams;
        }
    }
}