using BlackJackEnumerations;

namespace BlackJack
{
    public class Game
    {
        public ShuffleMachine Machine { get; private set; }
        public List<Player> Players { get; private set; }
        public Croupier Croupier { get; private set; }
        private Drawer drawer;

        public Game(List<Player> players, int countOfDecks = 8)
        {
            Players = players;
            drawer = new Drawer();
            Croupier = new Croupier();
            Machine = new ShuffleMachine(countOfDecks);
            Machine.Shuffle();
        }

        public void StartNewTurn()
        {
            foreach (Player player in Players)
            {
                player.GetNewHand(Machine);
                drawer.DrawPlayer(player);
                player.MakeBet(player.GetNewBet());
            }
            Croupier.GetNewHand(Machine);
        }

        public void StartActing()
        {
            foreach(Player player in Players)
            {
                PlayerMove action;
                do
                {
                    drawer.DrawPlayer(player);
                    action = player.GetMove();
                    switch (action)
                    {
                        case PlayerMove.Hit:
                            player.TookCard(Machine.TrowCard());
                            break;
                        case PlayerMove.Double:
                            player.Double(Machine.TrowCard());
                            break;
                    }
                } while (action != PlayerMove.Stand && player.Balance > 0 && Enumerable.Range(1, 21).Contains(player.GetScore()));
            }

            while (Enumerable.Range(1, 17).Contains(Croupier.GetScore()))
            {
                Croupier.TookCard(Machine.TrowCard());
            }
            drawer.DrawCroupier(Croupier);
        }

        public void EndTurn()
        {
            int CroupierScore = Croupier.GetScore();
            foreach (Player player in Players)
            {
                if (player.GetScore() == CroupierScore)
                {
                    player.TakeBetBack();
                }
                else if (player.GetScore() == 21)
                {
                    player.TakeBlackJackBet();
                }
                else if (player.GetScore() > CroupierScore || CroupierScore > 21)
                {
                    player.TakeNormalBet();
                }
                else
                {
                    player.LoseBet();
                }
            }
            Players = Players.Where(x => x.Balance > 0).ToList();
        }

        public void StartGame()
        {
            while (Players.Count > 0)
            {
                StartNewTurn();
                StartActing();
                EndTurn();
            }
        }

        public void StartGame(int countOfTurns)
        {
            int counter = 0;
            while (counter++ < countOfTurns && Players.Count > 0)
            {
                StartNewTurn();
                StartActing();
                EndTurn();
            }
        }
    }
}
