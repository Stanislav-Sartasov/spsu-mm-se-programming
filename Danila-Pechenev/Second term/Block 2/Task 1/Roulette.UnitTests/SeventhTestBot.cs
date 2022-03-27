namespace Roulette.UnitTests
{
    internal class SeventhTestBot : APlayer
    {
        internal bool? Won { get; private set; }
        internal int Count { get; private set; }

        public SeventhTestBot(int sum, Casino casino)
            : base(sum, casino)
        {
            Count = 0;
        }

        protected override Bet MakeBet()
        {
            switch (Count % 4)
            {
                case 0:
                    return new Bet(BetType.Colour, 0, MinBetAmount);
                case 1:
                    return new Bet(BetType.Parity, 0, MinBetAmount);
                case 2:
                    return new Bet(BetType.Dozen, 0, MinBetAmount);
                case 3:
                    return new Bet(BetType.Number, 36, MinBetAmount);
                default:
                    return new Bet(BetType.Number, 0, MinBetAmount);
            }
        }

        protected override void GiveResult(bool won)
        {
            Count++;
            Won = won;
        }
    }
}
