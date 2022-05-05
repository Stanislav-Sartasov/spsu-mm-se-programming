namespace ISites
{
    public interface ISite
    {
        public string Name { get; }
        public void ShowWeather();
        public Weather.Weather GetWeather();
    }
}