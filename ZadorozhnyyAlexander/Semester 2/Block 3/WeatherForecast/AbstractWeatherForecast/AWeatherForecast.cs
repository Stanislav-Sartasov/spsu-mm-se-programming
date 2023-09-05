using System.Reflection;

namespace AbstractWeatherForecast
{
    public abstract class AWeatherForecast
    {
        protected HttpClient client;
        protected AParser dataParser;

        protected const string saintPetesbergLat = "59.93863";
        protected const string saintPetesbergLon = "30.31413";

        protected bool isInitialized = false;

        public string CurrentCelsiusTemperature { get; protected set; }
        public string CurrentFahrenheitTemperature { get; protected set; }
        public string CloudCover { get; protected set; }
        public string Precipitation { get; protected set; }
        public string AirHumidity { get; protected set; }
        public string WildDirection { get; protected set; }
        public string WildSpeed { get; protected set; }

        protected abstract void ShowDescription();

        protected abstract void Initialize();

        public AWeatherForecast(HttpClient client)
        {
            this.client = client;
        }

        public void Update()
        {
            if (!isInitialized)
                Initialize();

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