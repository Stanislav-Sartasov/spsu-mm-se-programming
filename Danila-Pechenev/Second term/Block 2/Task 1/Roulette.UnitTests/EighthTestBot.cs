namespace Roulette.UnitTests
{
    internal class EighthTestBot : APlayer
    {
        public EighthTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override Bet MakeBet()
        {
            return new Bet(BetType.Colour, 0, MinBetAmount);
        }

        protected override void GiveResult(bool won)
        {
            return;
        }

        internal int GetMinimal() => MinBetAmount;

        internal int GetMaximal () => MaxBetAmount;
    }
}
