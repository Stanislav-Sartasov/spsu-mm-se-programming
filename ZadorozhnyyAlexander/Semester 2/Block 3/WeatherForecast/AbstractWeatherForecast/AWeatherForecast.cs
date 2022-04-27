using System.Reflection;

namespace AbstractWeatherForecast
{
    public abstract class AWeatherForecast
    {
        protected AParser dataParser;

        protected const double saintPetesbergLat = 59.93863;
        protected const double saintPetesbergLon = 30.31413;

        public string CurrentCelsiusTemperature { get; protected set; }
        public string CurrentFahrenheitTemperature { get; protected set; }
        public string CloudCover { get; protected set; }
        public string Precipitation { get; protected set; }
        public string AirHumidity { get; protected set; }
        public string WildDirection { get; protected set; }
        public string WildSpeed { get; protected set; }

        protected abstract void ShowDescription();

        public abstract void Initialize(HttpClient client);

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
            catch (NullReferenceException ex)
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