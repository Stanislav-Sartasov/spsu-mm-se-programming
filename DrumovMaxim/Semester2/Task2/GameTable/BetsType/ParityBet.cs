using GameTable.SectorTypeEnum;

namespace GameTable.BetsType
{
    public class ParityBet : Bet
    {
        public readonly ParityEnum SelectedSector;

        public ParityBet(int cash, ParityEnum parity) : base(cash) => SelectedSector = parity;

        public override int ReviewBet(SectorType winningSector) => SelectedSector == winningSector.Parity ? 2 : 0;

    }
}
