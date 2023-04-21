namespace GameTable.SectorTypeEnum
{
    public class SectorType
    {
        public int Number;
        public ColourEnum? Colour;   // Zero - null, Black - 1, Red - 2
        public ParityEnum? Parity;   // Zero - null, Even - 1, Odd - 2
        public DozenEnum? Dozen;     // Zero - null, FirstDozen - 1, SecondDozen - 2, ThirdDozen - 3

        public SectorType(int number, ColourEnum? colour, ParityEnum? parity, DozenEnum? dozen)
        {
            Number = number;
            Colour = colour;
            Parity = parity;
            Dozen = dozen;
        }
    }
}
