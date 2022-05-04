using WeatherLibrary;

namespace WeatherWebAPI
{
    public abstract class AParser
    {
        public string DefaultValue { get; protected set; } = "No Data";

        public abstract AWeather GetWeather();

        protected string TranslateCloudCover(string? cloudCoverage)
        {
            string stringToReturn = DefaultValue;

            try
            {
                float cloudCoverageProcent = float.Parse(cloudCoverage);

                if (cloudCoverageProcent < 25)
                {
                    stringToReturn = "Clear";
                }
                else if (cloudCoverageProcent < 50)
                {
                    stringToReturn = "Mostly Cloudy";
                }
                else
                {
                    stringToReturn = "Cloudy";
                }
            }
            catch (Exception e) { }

            return stringToReturn;
        }
    }
}