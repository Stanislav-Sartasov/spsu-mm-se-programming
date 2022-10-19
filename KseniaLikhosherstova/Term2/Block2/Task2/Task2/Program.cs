using Game.Players;
using Bots;



namespace Task2
{
    class Program
    {
        const int GamesCount = 40;

        static void Main(string[] args)
        {
            Console.WriteLine("The program demonstrates three strategies of the Blackjack");

            Player player1 = new StupidBot("Terry Benedict", 1500);
            Player player2 = new StandardBot("Rusty Ryan", 1500);
            Player player3 = new SmartBot("Danny Ocean", 1500);

            List<Player> players = new List<Player> { player1, player2, player3 };
            Game.BlackJack game = new Game.BlackJack(8, players);


            for (int i = 0; i < GamesCount; i++)
            {
                Console.WriteLine($"Round {i + 1} has started");

                game.PrepareRound();

                Console.WriteLine(game.GetMoneyInfo());
                game.PlaceBets();
                Console.WriteLine($"The players placed bets:");
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