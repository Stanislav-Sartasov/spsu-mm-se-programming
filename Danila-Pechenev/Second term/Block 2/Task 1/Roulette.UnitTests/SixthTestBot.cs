namespace Roulette.UnitTests
{
    internal class SixthTestBot : APlayer
    {
        public SixthTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Number, 0, MinBetAmount * 3 / 2);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
