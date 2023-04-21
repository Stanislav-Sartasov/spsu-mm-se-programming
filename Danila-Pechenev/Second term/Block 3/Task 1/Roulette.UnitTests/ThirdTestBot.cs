namespace Roulette.UnitTests
{
    internal class ThirdTestBot : APlayer
    {
        public ThirdTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Colour, 2, MinBetAmount);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }
    }
}
