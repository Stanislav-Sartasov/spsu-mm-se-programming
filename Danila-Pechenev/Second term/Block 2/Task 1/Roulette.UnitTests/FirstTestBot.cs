namespace Roulette.UnitTests
{
    internal class FirstTestBot : APlayer
    {
        public FirstTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Number, -5, MinBetAmount);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
