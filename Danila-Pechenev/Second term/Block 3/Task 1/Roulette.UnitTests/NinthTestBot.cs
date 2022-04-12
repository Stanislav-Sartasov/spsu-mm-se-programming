namespace Roulette.UnitTests
{
    internal class NinthTestBot : APlayer
    {
        public NinthTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
        }

        protected override bool WantsToPlay()
        {
            return true;
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
