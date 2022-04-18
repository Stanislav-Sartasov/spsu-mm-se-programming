using BlackjackBots;


namespace MartingaleBotLibrary
{
    public class MartingaleBot : UsualBaseStrategyBot
    {
        public MartingaleBot(double money, double startRate) : base(money, startRate) { }

        protected override void PrepareToNextGame()
        {
            Rate = IsWonLastGame ? StartRate : Rate * 2;
        }
    }
}