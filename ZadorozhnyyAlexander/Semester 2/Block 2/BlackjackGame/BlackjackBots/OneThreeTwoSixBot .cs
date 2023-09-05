namespace BlackjackBots
{
    public class OneThreeTwoSixBot : UsualBaseStrategyBot
    {
        private int[] multipleOneThreeTwoSixStrategy = { 1, 3, 2, 6 };
        private int nowIndex = 0;

        public OneThreeTwoSixBot(double money, double startRate) : base(money, startRate)
        {
            IsStandAfterFirstBlackjack = false;
        }

        protected override void PrepareToNextGame()
        {
            nowIndex = isWonLastGame ? (nowIndex + 1) % 4 : 0;
            Rate = startRate * multipleOneThreeTwoSixStrategy[nowIndex];
        }
    }
}