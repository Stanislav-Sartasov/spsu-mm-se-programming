using Game.Players;
using Game.Cards;
using System.Text;


namespace Game
{
    public class BlackJack
    {
        public const int DealerStandLimit = 17;
        public const int CardsCount = 2;

        private readonly int minCardsToShuffleDeck;
        private readonly int countOfDecks;

        private readonly Dealer dealer;
        private readonly List<Player> players;
        private readonly Dictionary<Player, int> bets;
        private readonly Dictionary<Player, PlayerAction> currentPlayerActions;

        private Deck deck;

        public BlackJack(int decksCount, List<Player> players)
        {
            dealer = new Dealer();
            this.players = players;
            bets = this.players.ToDictionary(k => k, v => 0);
            currentPlayerActions = new Dictionary<Player, PlayerAction>();

            deck = Deck.CreateDeck(decksCount);
            deck.Shuffle();

            countOfDecks = decksCount;
            minCardsToShuffleDeck = deck.Cards.Count / 3;
        }

        public bool IsEnded => players.All(x => x.State == PlayerState.Standing);


        public void PrepareRound()
        {
            for (int i = 0; i < players.Count; i++)
                if (players[i].Money <= 0)
                    players.Remove(players[i]);

            if (deck.Cards.Count < minCardsToShuffleDeck)
            {
                deck = Deck.CreateDeck(countOfDecks);
                deck.Shuffle();
            }

            dealer.ClearHand();
            players.ForEach(p => p.ClearHand());
        }


        public void PlaceBets()
        {
            players.ForEach(p =>
            {
                bets[p] = p.Bet();
            });
        }

        public void Deal()
        {
            for (int i = 0; i < CardsCount; i++)
            {
                players.ForEach(p => p.AddCard(deck.PickCard()));
                dealer.AddCard(deck.PickCard());
            }
        }

        public void PlayersMove()
        {
            foreach (var player in players)
            {
                var action = player.Move();

                currentPlayerActions[player] = action;

                switch (action)
                {
                    case PlayerAction.Hit:
                        {
                            player.Hit(deck.PickCard());
                            break;
                        }
                    case PlayerAction.Double:
                        {

                            player.Double(deck.PickCard(), bets[player]);
                            bets[player] *= 2;
                            break;
                        }
                    case PlayerAction.Stand:
                        {
                            player.Stand();
                            break;
                        }
                    default:
                        throw new InvalidOperationException(action.ToString());
                }
            }
        }

        public void DealerMove()
        {
            while (CanDealerHit(dealer))
                dealer.Hit(deck.PickCard());
        }

        public Dictionary<Hand, GameResult> FinishRound()
        {
            var results = GetRoundResult();

            foreach (var player in players)
            {
                if (IsBusted(player))
                    continue;

                if (results[player] == GameResult.PlayerWin && HasBlackJack(player))
                {
                    player.TakeMoney((int)(1.5 * bets[player]));
                }
                else if (results[player] == GameResult.PlayerWin)
                {
                    player.TakeMoney(2 * bets[player]);
                }
                else if (results[player] == GameResult.Push)
                {
                    player.TakeMoney(bets[player]);
                }
            }

            return results;
        }


        public Dictionary<Hand, GameResult> GetRoundResult()
        {
            var roundResult = new Dictionary<Hand, GameResult>();

            var dealerHandValue = dealer.GetValue();
            if (dealerHandValue > Hand.BlackJackHandValue)
                roundResult.Add(dealer, GameResult.DealerBusted);

            players.ForEach(p =>
            {
                var playerHandValue = p.GetValue();

                var gameResult = GameResult.PlayerWin;

                if (playerHandValue > Hand.BlackJackHandValue)
                    gameResult = GameResult.PlayerBusted;
                else if (playerHandValue == dealerHandValue)
                    gameResult = GameResult.Push;
                else if (playerHandValue < dealerHandValue && !IsBusted(dealer))
                    gameResult = GameResult.PlayerLost;

                roundResult.Add(p, gameResult);
            });

            return roundResult;
        }

        public bool CanDealerHit(Dealer dealer)
        {
            var value = dealer.GetValue();
            return value < DealerStandLimit;
        }

        public bool IsBusted(Hand hand)
        {
            var value = hand.GetValue();
            return value > Hand.BlackJackHandValue;
        }

        public bool HasBlackJack(Hand hand)
        {
            var value = hand.GetValue();
            return value == Hand.BlackJackHandValue;
        }

        public string GetMoneyInfo()
        {
            StringBuilder builder = new StringBuilder();

            players.ForEach(p =>
            {
                builder.AppendLine($"{p.Name}'s money: {p.Money}");
            });

            return builder.ToString();
        }

        public string GetPlayerActionInfo()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in currentPlayerActions)
            {
                builder.AppendLine($"{item.Key.Name}'s action: {item.Value}");
            }

            return builder.ToString();
        }

        public string GetCardsInfo()
        {
            StringBuilder builder = new StringBuilder();

            if (!IsEnded)
            {
                builder.AppendLine($"{dealer.Name}'s hand: {dealer.Cards.First()} ({dealer.GetValue()})");
            }
            else
            {
                builder.AppendLine($"{dealer.Name}'s hand: {string.Join(", ", dealer.Cards)} ({dealer.GetValue()})");
            }

            players.ForEach(p =>
            {
                builder.AppendLine($"{p.Name}'s hand: {string.Join(", ", p.Cards)} ({p.GetValue()})");
            });

            return builder.ToString();
        }

        public string GetBetsInfo()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var bet in bets)
            {
                builder.AppendLine($"{bet.Key.Name}'s bet: {bet.Value}");
            }

            return builder.ToString();
        }
    }
}
