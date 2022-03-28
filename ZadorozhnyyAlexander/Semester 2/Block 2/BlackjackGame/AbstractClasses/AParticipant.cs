namespace AbstractClasses
{
    public abstract class AParticipant
    {
        public List<ACard> CardsInHand;
        public int CountGames;
        public int CountWinGames;

        public abstract bool GetNextCard();

        public int GetSumOfCards()
        {
            int sum = 0;
            foreach (var card in this.CardsInHand)
                sum += card.CardNumber;
            return sum;
        }

        public virtual void Win()
        {
            throw new MissingMethodException();
        }

        public virtual void Lose()
        {
            throw new MissingMethodException();
        }
    }
}
