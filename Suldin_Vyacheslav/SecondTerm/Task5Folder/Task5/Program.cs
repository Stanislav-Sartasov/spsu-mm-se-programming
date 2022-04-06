using System;
using Newtonsoft.Json.Linq;

namespace Task5
{
   
    class Program
    {
        static void Main(string[] args)
        {

            WeatherRequest[] Set = new WeatherRequest[] {/* new StormGlass(), new OpenWeather(), new TomorrowIO(),*/ new GisMeteo() };
            Console.WriteLine("This app shows weather using web services\n If error (401) you can reset your invalid key: " +
                "For that on qustion ask: 'Number of your site to key reset' ('0' - just refresh, '-1' - kill)");
            string[] tableInfo = new string[] { "Web Service", "Temp (C)", "Temp (F)", "Cloud Cover", "Humidity", "Precipitation", "Wind Speed", "Wnid Direction" };
            foreach (string infoBar in tableInfo)
            {
                Console.Write(infoBar);
                Fill(infoBar.Length);
            }
            while (true)
            {
                int n = 1;
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------");
                foreach (WeatherRequest web in Set)
                {
                    Console.Write("\n" + web.ToString().Replace("Task5.", $"{n}: "));
                    n++;
                    Fill(web.ToString().Replace("Task5.", "").Length);
                    string[] answers = web.GetInfo();
                    if (answers.Length == 1)
                    {
                        Console.Write((ErrorType)Convert.ToInt32(answers[0]));
                    }
                    else
                    {
                        foreach (string answer in answers)
                        {
                            Console.Write(answer);
                            Fill(answer.Length);
                        }
                    }
   
                }
                Console.WriteLine("\nRefresh?");

                int userAnswer = GetCoorectAnswer(-1, n);

                if (userAnswer == -1)
                    break;
                else if (userAnswer != 0)
                {
                    Set[userAnswer - 1].SetKey();
                }
            }


            static void Fill(int n)
            {
                for (int i = 0; i < (15 - n); i++)
                    Console.Write(" ");
            }

            static int GetCoorectAnswer(int bottom, int top)
            {
                int answer;
                while (!int.TryParse(Console.ReadLine(), out answer) || answer > top || answer < bottom)
                    Console.WriteLine($"Error, enter {bottom}-{top}");
                return answer;
            }

        }
        
    }
}
