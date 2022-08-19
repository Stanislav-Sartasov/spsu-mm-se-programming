using GameTable.SectorTypeEnum;

namespace GameTable.BetsType
{
    public class NumberBet : Bet
    {
        public readonly int SelectedSector;

        public NumberBet(int cash, int number) : base(cash)
        {
            if(number >= 0 && number < 37)
            {
                SelectedSector = number;
            } 
            else throw new ArgumentOutOfRangeException();
        }

        public override int ReviewBet(SectorType winningSector) => SelectedSector == winningSector.Number ? 36 : 0;

    }
}