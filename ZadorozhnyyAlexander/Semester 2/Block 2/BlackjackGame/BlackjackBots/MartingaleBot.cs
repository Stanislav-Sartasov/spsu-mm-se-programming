namespace BlackjackBots
{
    public class MartingaleBot : UsualBaseStrategyBot
    {
        public MartingaleBot(double money, double startRate) : base(money, startRate) { }

        protected override void PrepareToNextGame()
        {
            Rate = isWonLastGame ? startRate : Rate * 2;
        }
    }
}