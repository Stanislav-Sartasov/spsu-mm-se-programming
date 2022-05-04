namespace AbstractWeatherForecast
{
    public abstract class AParser
    {
        protected List<string> resultJsonValues = new List<string>();

        public abstract List<string> GetListOfCurrentData();
    }
}
