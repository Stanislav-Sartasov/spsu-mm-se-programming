namespace DataGetter;
using System.Text.Json;

public class OpenWeatherMapGetter : ADefaultWeatherGetter
{
    public override string Name { get; } = "OpenWeatherMap";

    protected OpenWeatherMapGetter(string key)
        : base(key)
    {
    }

    public static OpenWeatherMapGetter CreateGetter(string pathToFileWithKey)
    {
        using var stream = File.OpenRead(pathToFileWithKey);
        var jsonData = JsonDocument.Parse(stream);
        var root = jsonData.RootElement;
        string key = root.GetProperty("OpenWeatherMap").GetString() ?? "wrongKey";

        return new OpenWeatherMapGetter(key);
    }

    public override string[]? ParseResponseStream(Stream responseStream, string[] fields)
    {
        string[] result = new string[fields.Length];

        var responseData = JsonDocument.Parse(responseStream);
        var weatherNowData = responseData.RootElement;

        for (int i = 0; i < fields.Length; i++)
        {
            string value;
            if (fields[i].Contains('.'))
            {
                string[] nextFields = fields[i].Split('.');
                var currentField = weatherNowData.GetProperty(nextFields[0]);
                if (currentField.ToString()[0] == '[')
                {
                    currentField = currentField[0];
                }

                value = currentField.GetProperty(nextFields[1]).GetRawText();
            }
            else
            {
                value = weatherNowData.GetProperty(fields[i]).GetRawText();
            }

            result[i] = value.Replace("\"", "");
        }

        responseStream.Close();

        return result;
    }

    protected override string GenerateURL(double[] locationСoordinates, string[] fields)
    {
        string url = "https://api.openweathermap.org/data/2.5/weather?lat=";
        url += locationСoordinates[0].ToString().Replace(",", ".");
        url += "&lon=";
        url += locationСoordinates[1].ToString().Replace(",", ".");
        url += $"&appid={APIKey}&units=metric";

        return url;
    }
}
