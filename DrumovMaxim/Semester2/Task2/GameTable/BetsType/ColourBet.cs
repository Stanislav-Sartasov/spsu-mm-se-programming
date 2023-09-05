using GameTable.SectorTypeEnum;

namespace GameTable.BetsType
{
    public class ColourBet : Bet
    {
        public readonly ColourEnum SelectedSector;

        public ColourBet(int cash, ColourEnum colour) : base(cash) => SelectedSector = colour;
   
        public override int ReviewBet(SectorType winningSector) => SelectedSector == winningSector.Colour ? 2 : 0;
        
    }
}
