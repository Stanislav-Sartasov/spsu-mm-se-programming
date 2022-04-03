using BlackJackEnumerations;

namespace BlackJack
{
    public class Player : APerson
    {
        public string Name { get; private set; }
        public int Balance { get; private set; }
        public int Bet { get; private set; }

        public Player(string name, int balance)
        {
            Cards = new List<Card>();
            Name = name;
            Balance = balance;
            Bet = 0;
        }

        public virtual PlayerMove GetMove()
        {
            string action;
            int playerMove;
            while (true)
            {
                Console.WriteLine("Your action: ");
                action = Console.ReadLine();
                if (action.ToCharArray().All(x => Char.IsDigit(x)))
                {
                    playerMove = Int32.Parse(action);
                    if (Enumerable.Range(0, 3).Contains(playerMove))
                    {
                        break;
                    }
                }
            }

            return (PlayerMove)playerMove;
        }

        public virtual int GetNewBet()
        {
            string bet;
            do
            {
                Console.WriteLine("Your bet: ");
                bet = Console.ReadLine();

            } while (!bet.ToCharArray().All(x => Char.IsDigit(x)));

            return Int32.Parse(bet);
        }

        public void MakeBet(int bet)
        {
            if (bet < Balance)
            {
                Balance -= bet;
                Bet += bet;
            }
            else
            {
                Bet += Balance;
                Balance = 0;
            }
        }

        public void Double(Card card)
        {
            MakeBet(Bet);
            TookCard(card);
        }

        public void LoseBet()
        {
            Bet = 0;
        }

        public void TakeNormalBet()
        {
            Balance += 2 * Bet;
            Bet = 0;
        }

        public void TakeBlackJackBet()
        {
            Balance += 5 * Bet / 2;
            Bet = 0;
        }

        public void TakeBetBack()
        {
            Balance += Bet;
            Bet = 0;
        }
    }
}
