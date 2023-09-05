namespace DataGetter;

 public interface IWeatherGetter
{
    public string Name { get; }
    string[]? GetWeather(double[] locationСoordinates, string[] fields);
}
