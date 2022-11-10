using GameTable.SectorTypeEnum;

namespace GameTable.BetsType
{
    public abstract class Bet
    {
        public readonly int value;

        public Bet(int value)
        {
            this.value = value;
        }

        public abstract int ReviewBet(SectorType winningSector);
        
    }
}
