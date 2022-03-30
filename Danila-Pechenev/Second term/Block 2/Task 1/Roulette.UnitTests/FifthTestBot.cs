namespace Roulette.UnitTests
{
    internal class FifthTestBot : APlayer
    {
        public FifthTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Number, 13, MaxBetAmount * 3 / 2);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
