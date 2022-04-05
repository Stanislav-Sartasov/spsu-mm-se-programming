using ISites;
using Sites;

namespace Task_2
{
    class Programm
    {
        static void Main(string[] args)
        {
            ISite site = new OpenWeatherMap();
            site.ShowWeather();
        }
    }
}