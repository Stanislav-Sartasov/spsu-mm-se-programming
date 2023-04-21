using ToolKit;

namespace Bots
{
    public sealed class PlusMinusBot : ACountingBot
    {
        public PlusMinusBot(int money = 10000, int games = 40) : base(money, games)
        {

        }

        private protected override int CountCard(Card card)
        {
            if (card.GetPoints() >= 10)
            {
                return -1;
            }
            else if (card.GetPoints() >= 7)
            {
                return 0;
            }

            return 1;
        }

        public override void MakeBet(int bet)
        {
            if (realAccount < 0)
            {
                countingBet = countingBet / 2 >= bet ? countingBet / 2 : bet;
            }
            else if (realAccount > 0)
            {
                countingBet = countingBet * 2 > 0 ? countingBet * 2 : bet * 2;
            }
            else
            {
                countingBet = bet * 16;
            }

            base.MakeBet(countingBet);
        }
    }
}