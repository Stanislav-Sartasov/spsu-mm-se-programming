using Cards;

namespace BotStructure
{
    public abstract class Bot : IBot
    {
        public int Balance { get; set; }

        public List<Hand> Hands { get; set; }

        public int CurrentBet { get; private set; }

        protected const int MinBet = 2;

        private int splitCount;

        protected Bot(int balance)
        {
            Balance = balance;
            Hands = new List<Hand>();
            Hands.Add(new Hand(0));
            CurrentBet = MinBet * 11;
            Hands[0].Bet = CurrentBet;
        }

        protected abstract int NewBet(Shoes shoes);

        public void Play(Card croupierOpenCard, Shoes shoes)
        {
            Hands[0].Bet = CurrentBet;

            do
            {
                splitCount = 0;
                for (int i = 0; i < Hands.Count; i++)
                {
                    Move(i, croupierOpenCard, shoes);
                }
            }
            while (splitCount != 0);

            CurrentBet = NewBet(shoes);
        }

        public void Move(int currentHand, Card croupierOpenCard, Shoes shoes)
        {
            Hand hand = Hands[currentHand];

            while (hand.Cards.Count < 2)
            {
                hand.Cards.Add(shoes.GetCard());
            }

            if (hand.Cards.Count == 2)
            {
                if (SplitCondition(hand, croupierOpenCard))
                {
                    splitCount += 2;
                    Split(currentHand);
                    return;
                }
                if (DoubleDownCondition(hand, croupierOpenCard))
                {
                    DoubleDown(shoes, currentHand);
                    return;
                }
            }

            while (TakingCardCondition(hand, croupierOpenCard))
                hand.Cards.Add(shoes.GetCard());
        }

        private bool TakingCardCondition(Hand hand, Card croupierOpenCard)
        {
            int handValue = hand.GetHandValue();

            if (hand.ContainsAce())
            {
                if (handValue == 18 && (int)croupierOpenCard.Rank >= 9)
                    return true;
                if (handValue >= 12 && handValue <= 17)
                    return true;
            }

            if (handValue >= 4 && handValue <= 11)
                return true;
            if (handValue == 12 && ((int)croupierOpenCard.Rank < 4 || (int)croupierOpenCard.Rank > 6))
                return true;
            if (handValue >= 13 && handValue <= 16 && (int)croupierOpenCard.Rank > 6)
                return true;

            return false;
        }

        private void Split(int currentHand)
        {
            Hand newHand = new Hand(Hands[currentHand].Bet);
            newHand.Cards = new List<Card>();
            newHand.Cards.Add(Hands[currentHand].Cards[0]);
            
            Hands.Remove(Hands[currentHand]);

            Hands.Add(newHand);
            Hands.Add((Hand)newHand.Clone());
        }

        private bool SplitCondition(Hand hand, Card croupierOpenCard)
        {
            if (hand.Cards[0].Rank != hand.Cards[1].Rank || hand.Bet > Balance)
                return false;

            switch (hand.Cards[0].GetCardRank())
            {
                case 11:
                case 8:
                    if (croupierOpenCard.Rank != CardRank.Ace)
                        return true;
                    break;
                case 9:
                    if ((int)croupierOpenCard.Rank != 7 && (int)croupierOpenCard.Rank < 10)
                        return true;
                    break;
                case 7:
                case 3:
                case 2:
                    if ((int)croupierOpenCard.Rank < 8)
                        return true;
                    break;
                case 6:
                    if ((int)croupierOpenCard.Rank < 7)
                        return true;
                    break;
                case 4:
                    if (croupierOpenCard.Rank == CardRank.Five || croupierOpenCard.Rank == CardRank.Six)
                        return true;
                    break;
            }

            return false;
        }

        private void DoubleDown(Shoes shoes, int currentHand)
        {
            Hands[currentHand].Cards.Add(shoes.GetCard());
            Hands[currentHand].Bet *= 2;
        }

        private bool DoubleDownCondition(Hand hand, Card croupierOpenCard)
        {
            if (hand.Bet > Balance)
                return false;

            int handValue = hand.GetHandValue();

            if (handValue == 9 && (int)croupierOpenCard.Rank > 2 && (int)croupierOpenCard.Rank < 7)
                return true;
            if (handValue == 10 && (int)croupierOpenCard.Rank < 10)
                return true;
            if (handValue >= 13 && handValue <= 17 && hand.ContainsAce() && (croupierOpenCard.Rank == CardRank.Five || croupierOpenCard.Rank == CardRank.Six))
                return true;

            return false;
        }
    }
}