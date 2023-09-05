namespace AbstractClasses
{
    public abstract class AParticipant
    {
        public int CountGames { get; protected set; }
        public int CountWinGames { get; protected set; }
        public List<ACard> CardsInHand { get; protected set; }
        public PlayerTurn PlayerTurnNow { get; protected set; }

        public abstract bool IsNeedNextCard();

        public int GetSumOfCards()
        {
            int sum = 0;
            foreach (var card in this.CardsInHand)
                sum += card.CardNumber;
            return sum;
        }

        public virtual void TakeCard(ACard card)
        {
            this.CardsInHand.Add(card);
        }

        public virtual void Win()
        {
            CountGames++;
            CountWinGames++;
        }

        public virtual void Lose()
        {
            CountGames++;
        }

        public virtual void Push()
        {
            CountGames++;
        }
    }
}
