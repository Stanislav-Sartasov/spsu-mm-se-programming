using System.Reflection;

namespace AbstractWeatherForecast
{
    public abstract class AWeatherForecast
    {
        protected AParser dataParser;

        public string CurrentCelsiusTemperature { get; protected set; }
        public string CurrentFahrenheitTemperature { get; protected set; }
        public string CloudCover { get; protected set; }
        public string Precipitation { get; protected set; }
        public string AirHumidity { get; protected set; }
        public string WildDirection { get; protected set; }
        public string WildSpeed { get; protected set; }

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

        public void ShowFullWeatherForecast()
        {
            ShowDescription();
            foreach (PropertyInfo info in this.GetType().GetProperties())
            {
                var value = info.GetValue(this);
                Console.WriteLine($"{info.Name} ---> {value}");
            }
            Console.WriteLine("\n");
        }
    }
}