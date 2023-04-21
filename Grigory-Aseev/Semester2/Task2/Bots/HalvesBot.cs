using ToolKit;

namespace Bots
{
    public class HalvesBot : ACountingBot
    {
        public HalvesBot(int money = 10000, int games = 40) : base(money, games)
        {

        }

        private protected override int CountCard(Card card)
        {
            return card.GetPoints() switch
            {
                2 => 1,
                3 => 2,
                4 => 2,
                5 => 3,
                6 => 2,
                7 => 1,
                8 => 0,
                9 => -1,
                _ => -2
            };
        }

        public override void MakeBet(int bet)
        {
            realAccount /= 2;

            if (realAccount < 0)
            {
                countingBet = countingBet / 4 >= bet ? countingBet / 4 : bet;
            }
            else if (realAccount > 0)
            {
                countingBet = countingBet * 4 > 0 ? countingBet * 4 : bet * 4;
            }
            else
            {
                countingBet = bet * 16;
            }

            base.MakeBet(countingBet);
        }
    }
}