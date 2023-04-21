namespace Roulette.UnitTests
{
    internal class FourthTestBot : APlayer
    {
        public FourthTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Parity, 0, MinBetAmount / 2);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
