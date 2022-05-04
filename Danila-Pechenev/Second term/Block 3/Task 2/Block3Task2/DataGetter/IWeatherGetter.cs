namespace DataGetter;

 public interface IWeatherGetter
{
    string[]? GetWeather(double[] locationСoordinates, string[] fields);
}
