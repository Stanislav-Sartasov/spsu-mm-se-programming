using RequestApi;

namespace AbstractWeatherForecast
{
    public abstract class AParser
    {
        protected ApiHelper apiHelper;
        protected List<string> resultJsonValues = new List<string>();

        public abstract List<string> GetListOfCurrentData();

        public AParser(ApiHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }
    }
}
