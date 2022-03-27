namespace Roulette.UnitTests
{
    internal class SecondTestBot : APlayer
    {
        public SecondTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Dozen, 1, MinBetAmount);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
