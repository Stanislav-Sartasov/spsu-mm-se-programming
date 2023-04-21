using WeatherManagerAPI;

namespace Weather
{
    class Program
    {
        public static void Main()
        {
            WeatherPrinter.SetWindowSize();

            List<AManagerAPI> siteList = new List<AManagerAPI>();
            siteList.Add(new TomorrowIO());
            siteList.Add(new StormGlassIO());

            for (int i = 0; i < siteList.Count; i++)
            {
                siteList[i].EmptyPattern();
            }

            do
            {
                Console.Clear();
                WeatherPrinter.PrintGreetings();

                for (int i = 0; i < siteList.Count; i++)
                {
                    WeatherPrinter.WeatherOutput(siteList[i]);
                    siteList[i].EmptyPattern();
                }

                Console.WriteLine("Press Ecs to exit or any other button to update the weather\n...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}