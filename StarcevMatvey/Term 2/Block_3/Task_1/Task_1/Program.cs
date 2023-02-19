using BlackJack;
using static System.Console;
using BotLoader;

namespace Task_1
{
    class Progaram
    {
        static void Main(string[] args)
        {
            int counfOfGames = 100;
            int countOfTurns = 40;

            Write("Enter the path to the folder with the libraries of bots.\n> ");
            string path = ReadLine();

            Loader loader = new Loader();
            List<Player> players = loader.LoadAllBotsFromDirectory(path);
            if (players == null)
            {
                return;
            }

            List<int> sums = new List<int>().Concat(Enumerable.Range(0, players.Count)).Select(x => 0).ToList();

            for (int _ = 0; _ < counfOfGames; _++)
            {
                Game blackJack = new Game(players);
                blackJack.StartGame(countOfTurns);
                sums = sums.Select((x, y) => x += players[y].Balance).ToList();
                players = loader.LoadAllBotsFromDirectory(path);

            }

            ReadKey();

            Clear();

            for (int i = 0; i < players.Count; i++)
            {
                WriteLine(players[i].Name + ": " + sums[i] / counfOfGames);
            }

            ReadKey();
        }
    }
}