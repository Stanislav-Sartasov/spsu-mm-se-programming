using StormGlassWeatherServiceLib;
using WeatherServiceLib;
using OpenWeatherMapServiceLib;

class Program
{
    static void Main()
    {
        PrintDescription();
        Boolean programRunning = true;

        AbstractWeatherService firstWeatherService = new StormGlassWeatherService(59.9311, 30.3609, "3a671f4a-be66-11ec-9d13-0242ac130002-3a671fc2-be66-11ec-9d13-0242ac130002");
        AbstractWeatherService secondWeatherService = new OpenWeatherMapService(59.9311, 30.3609, "83287654f9b418ab802771fac776a42f");

        while (programRunning)
        {
            Console.WriteLine("Введите команду:");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    Console.WriteLine("Погода от stormglass.io:");
                    firstWeatherService.PrintInfo();
                    break;
                case "2":
                    Console.WriteLine("Погода от openweathermap.org:");
                    secondWeatherService.PrintInfo();
                    break;
                case "u":
                    if (firstWeatherService.UpdateInfo())
                    {
                        Console.WriteLine("Данные сервиса 1 были обновлены.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Не удалось обновить данные сервиса 1.");
                        Console.WriteLine("Это могло произойти из-за исчерпания дневных запросов для ключа, или же неполадок на стороне сайта.");
                    }
                    if (secondWeatherService.UpdateInfo())
                    {
                        Console.WriteLine("Данные сервиса 2 были обновлены.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Не удалось обновить данные сервиса 2.");
                        Console.WriteLine("Это могло произойти из-за исчерпания дневных запросов для ключа, или же неполадок на стороне сайта.");
                    }
                    break;
                case "q":
                    programRunning = false;
                    Console.WriteLine("Выход из программы...");
                    break;
                case "h":
                    PrintDescription();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда! Попробуйте ещё раз!");
                    break;
            }
        }
    }

    static void PrintDescription()
    {
        Console.WriteLine("Данная программа выведет информацию о погоде в Санкт-Петербурге.");
        Console.WriteLine("Используемые сервисы: stormglass.io и openweathermap.org/api");
        Console.WriteLine("Доступные команды:");
        Console.WriteLine("q - Выход из программы");
        Console.WriteLine("u - Обновить данные");
        Console.WriteLine("1 - Информация о погоде от stormglass.io");
        Console.WriteLine("2 - Информация о погоде от openweathermap.org");
        Console.WriteLine("h - Вывести описание программы");
    }
}