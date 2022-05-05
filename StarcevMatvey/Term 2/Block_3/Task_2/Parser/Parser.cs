using System.Text.RegularExpressions;

namespace Tools
{
    public class Parser
    {
        readonly string errorValue = "No data";
        readonly string messenge;

        public Parser(string messenge)
        {
            this.messenge = messenge;
        }

        public Weather.Weather Parse(List<string> patterns)
        {
            string tempC = GetFirstStringWithRegex(patterns[0]).Replace(".", ",");
            string tempF = tempC != errorValue ? (1.8 * Double.Parse(tempC) + 32).ToString("0.00") : tempC;
            string clouds = GetFirstStringWithRegex(patterns[1]);
            string humidity = GetFirstStringWithRegex(patterns[2]);
            string windSpeed = GetFirstStringWithRegex(patterns[3]);
            string windDegree = GetFirstStringWithRegex(patterns[4]);
            string fallOut = GetFirstStringWithRegex(patterns[5]);

            return new Weather.Weather(tempC, tempF, clouds, humidity, windSpeed, windDegree, fallOut);

        }

        private string GetFirstStringWithRegex(string pattern)
        {
            MatchCollection matchCollection = new Regex(@pattern).Matches(messenge);
            return matchCollection.Count !=0 ? matchCollection.First().Value.ToString() : errorValue;
        }
    }
}