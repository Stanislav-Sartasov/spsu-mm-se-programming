namespace BlackjackBots
{
    public abstract class APlayer
    {
        public byte Score { get; private set; }
        public float Balance { get; private set; }

        public APlayer(float startBalance)
        {
            this.Balance = startBalance;
            this.Score = 0;
        }

        abstract public bool DoesHit(byte croupiersCardWeight);

        public void ResetScore()
        {
            this.Score = 0;
        }

        public void WinCasual(float betValue)
        {
            this.Balance += betValue;
        }

        public void WinBlackjack(float betValue)
        {
            this.Balance += betValue * 1.5F;
        }

        public void Lose(float betValue)
        {
            this.Balance -= betValue;
        }

        public void IncreaseScore(byte weight)
        {
            if (weight == 11 && this.Score > 10) 
            {
                this.Score += 1;
            }
            else
            {
                this.Score += weight;
            }
        }
        public bool IsBlackjack() => this.Score == 21;

        public bool IsBust() => this.Score > 21;
    }
}