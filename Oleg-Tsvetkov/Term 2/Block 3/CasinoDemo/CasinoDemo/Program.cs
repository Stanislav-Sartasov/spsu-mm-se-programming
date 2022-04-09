using System;
using CasinoLib;
using RandomParityBotLib;
using MartingaleBotLib;
using YoloBotLib;
using BotPluginsLib;

namespace Casino
{
    public class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Данная программа демонстрирует работу загрузки плагинов(dll), являющихся ботами.");
            Console.WriteLine("Загрузка происходит из пути папки, содержащего ботов, указанного, как первый аргумент.");

            Int64 startBalance = 1000;

            if (args.Length != 1)
            {
                Console.WriteLine("Неверное количество аргументов. Требуется только указать путь к папке с плагинами.");
                return;
            }

            BotPluginLoader loader = new(args[0], startBalance);

            Console.WriteLine("Демонстрация ботов:");
            Roulette roulette = new Roulette();
            for (int i = 0; i < loader.LoadedBots.Count; ++i)
            {
                Console.WriteLine("Бот "+(i+1).ToString()+". Начальный баланс: "+startBalance.ToString());
                BotPlayer botPlayer = loader.LoadedBots[i];
                botPlayer.PlayAverageBetsForBot(roulette, 40);
                Double botAverage = botPlayer.Balance;
                Console.WriteLine("В среднем после сорока ставок новый баланс: " + botAverage);
                Console.WriteLine("В среднем бот потерял за ход: " + (1000f - botAverage) / 40f);
            }
        }
    }
}