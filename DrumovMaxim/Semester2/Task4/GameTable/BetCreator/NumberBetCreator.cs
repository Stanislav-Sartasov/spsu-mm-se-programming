using GameTable.BetsType;

namespace GameTable.BetCreator
{
    public class NumberBetCreator : IBetCreator
    {
        public int cash { get; set; }

        public Bet FormBet()
        {
            int number = new Random().Next(0, 36);
            return new NumberBet(cash, number);
        }
    }
}
