namespace DataGetter;
using System.Net;

public abstract class ADefaultWeatherGetter : IWeatherGetter
{
    public abstract string Name { get; }

    protected readonly string APIKey;
    protected ADefaultWeatherGetter(string key)
    {
        APIKey = key;
    }

    public string[]? GetWeather(double[] locationСoordinates, string[] fields)
    {
        string URL = GenerateURL(locationСoordinates, fields);
        var request = WebRequest.Create(URL);

        try
        {
            using var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            return ParseResponseStream(responseStream, fields);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unauthorized"))
            {
                return null;
            }

            string[] result = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                result[i] = "No data";
            }

            return result;
        }
    }

    public abstract string[]? ParseResponseStream(Stream responseStream, string[] fields);

    protected abstract string GenerateURL(double[] locationСoordinates, string[] fields);
}
