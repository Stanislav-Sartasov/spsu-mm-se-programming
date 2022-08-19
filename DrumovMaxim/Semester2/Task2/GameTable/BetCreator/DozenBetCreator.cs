using GameTable.BetsType;
using GameTable.SectorTypeEnum;

namespace GameTable.BetCreator
{
    public class DozenBetCreator : IBetCreator
    {
        public int Cash { get; set; }
        static Random rnd = new Random();

        public Bet FormBet()
        {
            var value = rnd.RandomEnumVal<DozenEnum>();
            return new DozenBet(Cash, value);
        }
    }
}
