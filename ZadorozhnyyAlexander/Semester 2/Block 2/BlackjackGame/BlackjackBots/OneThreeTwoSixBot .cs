namespace BlackjackBots
{
    public class OneThreeTwoSixBot : UsualBaseStrategyBot
    {
        private int[] MultipleOThreeTwoSixStrategy = { 1, 3, 2, 6 };
        private int NowIndex = 0;

        public OneThreeTwoSixBot(double money, double startRate) : base(money, startRate)
        {
            IsStandAfterFirstBlackjack = false;
        }

        protected override void PrepareToNextGame()
        {
            NowIndex = IsWonLastGame ? (NowIndex + 1) % 4 : 0;
            Rate = StartRate * MultipleOThreeTwoSixStrategy[NowIndex];
        }
    }
}