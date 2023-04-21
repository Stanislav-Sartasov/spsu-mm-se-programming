namespace BlackjackBots
{
    public class BaseStrategyBot : APlayer
    {
        public BaseStrategyBot(float startBalance) : base(startBalance)
        {
        }

        public override bool DoesHit(byte croupierCard)
        {
            if ((this.Score < 12) ||
                (this.Score == 12 && croupierCard < 5) ||
                ((this.Score < 15 && this.Score > 12) && (croupierCard < 7 && croupierCard > 2)) ||
                (this.Score < 17 && croupierCard > 6))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}