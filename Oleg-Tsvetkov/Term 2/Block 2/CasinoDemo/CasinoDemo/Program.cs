using System;
using CasinoLib;
using CasinoBotsLib;

namespace Casino
{
    public class Program
    {
        static void Main()
        {
            Roulette roulette = new Roulette();
            Console.WriteLine("Данная программа демонстрирует работу рулетки и трёх ботов, использующих различные стратегии");
            Console.WriteLine("Демонстрация ботов:");
            Console.WriteLine("Бот 1: Бот, делающий ставки на случайную чётность. Начальный баланс: 1000");
            BotPlayer firstBotPlayer = new RandomParityBotPlayer(1000);
            firstBotPlayer.PlayAverageBetsForBot(roulette, 40);
            Double firstBotAverage = firstBotPlayer.Balance;
            Console.WriteLine("В среднем после сорока ставок новый баланс: " + firstBotAverage);
            Console.WriteLine("В среднем бот потерял за ход: " + (1000f - firstBotAverage) / 40f);
            Console.WriteLine("Бот 2: Бот, делающий ставки на цвет по стратегии Мартингейла. Начальный баланс: 1000");
            Console.WriteLine("Бот делает ставку на случайный цвет(красный или чёрный). При проигрыше ставит в два раза больше на тот же цвет, удваивая ставку до победы/потери баланса");
            Console.WriteLine("Если боту не хватило денег, ставка равна балансу.");
            BotPlayer secondBotPlayer = new MartingaleBotPlayer(1000);
            secondBotPlayer.PlayAverageBetsForBot(roulette, 40);
            Double secondBotAverage = secondBotPlayer.Balance;
            Console.WriteLine("В среднем после сорока ставок новый баланс: " + secondBotAverage);
            Console.WriteLine("В среднем бот потерял за ход: " + (1000f - secondBotAverage) / 40f);
            Console.WriteLine("Бот 3: Бот, делающий ставку на конкретное число до победы. При победе меняет число на новое.");
            BotPlayer thirdBotPlayer = new YoloBotPlayer(1000);
            thirdBotPlayer.PlayAverageBetsForBot(roulette, 40);
            Double thirdBotAverage = secondBotPlayer.Balance;
            Console.WriteLine("В среднем после сорока ставок новый баланс: " + thirdBotAverage);
            Console.WriteLine("В среднем бот потерял за ход: " + (1000f - thirdBotAverage) / 40f);
        }
    }
}