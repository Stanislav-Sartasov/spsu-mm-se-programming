using GameTable.SectorTypeEnum;

namespace GameTable.BetsType
{
    public class DozenBet : Bet
    {
        public readonly DozenEnum SelectedSector;

        public DozenBet(int cash, DozenEnum dozen) : base(cash) => SelectedSector = dozen;

        public override int ReviewBet(SectorType winningSector) => SelectedSector == winningSector.Dozen ? 3 : 0;

    }
}
