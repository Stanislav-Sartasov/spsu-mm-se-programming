using Game.Players;
using LibraryLoader;



namespace Task4
{
    class Program
    {
        const int GamesCount = 40;

        static void Main(string[] args)
        {
            Console.WriteLine("The program demonstrates three strategies of the Blackjack game");

            if (args.Length != 1)
            {
                Console.WriteLine("An error occurred while entering the path to the library");
                return;
            }

            Loader loaderL = new Loader();
            var player = Loader.LoadBots(args[0]);
            List<Player> players = new List<Player>();
            foreach (var p in player)
                players.Add(p);
            Game.BlackJack game = new Game.BlackJack(8, players);


            Console.WriteLine("Welcome to BlackJack game!");

            for (int i = 0; i < GamesCount; i++)
            {
                Console.WriteLine($"Round {i + 1} has started");

                game.PrepareRound();

                Console.WriteLine(game.GetMoneyInfo());
                game.PlaceBets();
                Console.WriteLine($" Bets:");
                Console.WriteLine(game.GetBetsInfo());

                game.Deal();

                while (true)
                {
                    Console.WriteLine(game.GetCardsInfo());
                    game.PlayersMove();
                    Console.WriteLine(game.GetPlayerActionInfo());

                    if (game.IsEnded)
                    {
                        game.DealerMove();
                        Console.WriteLine(game.GetCardsInfo());
                        break;
                    }
                }

                Console.WriteLine("Round results:");
                var roundResult = game.FinishRound();

                foreach (var item in roundResult)
                {
                    Console.WriteLine($"{item.Key.Name} - {item.Value}");
                }

                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine();
            }




        }
    }
}