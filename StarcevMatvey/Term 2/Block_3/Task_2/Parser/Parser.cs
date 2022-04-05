namespace Parser
{
    public abstract class Parser
    {
        public static Weather.Weather Parse(string messenge, List<string> parametrs)
        {
            string tempC = GetStringBetween(messenge, parametrs[0], parametrs[1]);
            string tempF = (1.8 * Double.Parse(tempC.Replace('.', ',')) + 32).ToString("0:00");
            string clouds = GetStringBetween(messenge, parametrs[2], parametrs[3]);
            string humidity = GetStringBetween(messenge, parametrs[4], parametrs[5]);
            string windSpeed = GetStringBetween(messenge, parametrs[6], parametrs[7]);
            string windDegree = GetStringBetween(messenge, parametrs[8], parametrs[9]);
            string fallOut = GetStringBetween(messenge, parametrs[10], parametrs[11]);

            return new Weather.Weather(tempC, tempF, clouds, humidity, windSpeed, windDegree, fallOut);
        }

        private static string GetStringBetween(string messenge, string start, string end)
        {
            int s = messenge.IndexOf(start) + start.Length;
            if (s < 0) return "No data";
            int e = messenge.IndexOf(end, s);
            return messenge.Substring(s, e - s);
        }
    }
}