namespace DataGetter;
using System.Text.Json;

public class TomorrowIoWeatherGetter : ADefaultWeatherGetter
{
    public override string Name { get; } = "TomorrowIo";

    protected const string pathToMetricValuesFile = "../../../../DataGetter/MetricValues.json";

    protected TomorrowIoWeatherGetter(string key)
        : base(key)
    {
    }

    public static TomorrowIoWeatherGetter CreateGetter(string pathToFileWithKey)
    {
        using var stream = File.OpenRead(pathToFileWithKey);
        var jsonData = JsonDocument.Parse(stream);
        var root = jsonData.RootElement;
        string key = root.GetProperty("TomorrowIo").GetString() ?? "wrongKey";

        return new TomorrowIoWeatherGetter(key);
    }

    public override string[]? ParseResponseStream(Stream responseStream, string[] fields)
    {
        string[] result = new string[fields.Length];
        var metricValuesStream = File.OpenRead(pathToMetricValuesFile);

        var responseData = JsonDocument.Parse(responseStream);
        var responseRoot = responseData.RootElement;
        var weatherNowData = responseRoot.GetProperty("data").GetProperty("timelines")[0]
            .GetProperty("intervals")[0].GetProperty("values");

        var metricValuesData = JsonDocument.Parse(metricValuesStream);
        var fieldsData = metricValuesData.RootElement;

        for (int i = 0; i < fields.Length; i++)
        {
            string value = weatherNowData.GetProperty(fields[i]).GetRawText();
            result[i] = value;
            if (fieldsData.GetProperty(fields[i]).ValueKind == JsonValueKind.Object)
            {
                result[i] = fieldsData.GetProperty(fields[i]).GetProperty(value).GetString() ?? "No data";
            }
        }

        metricValuesStream.Close();
        responseStream.Close();

        return result;
    }

    protected override string GenerateURL(double[] locationСoordinates, string[] fields)
    {
        string url = "https://api.tomorrow.io/v4/timelines?location=";
        url += locationСoordinates[0].ToString().Replace(",", ".");
        url += ',';
        url += locationСoordinates[1].ToString().Replace(",", ".");
        url += "&fields=";
        for (int i = 0; i < fields.Length - 1; i++)
        {
            url += fields[i];
            url += ",";
        }

        url += fields[^1];
        url += "&units=metric&apikey=";
        url += APIKey;

        return url;
    }
}
