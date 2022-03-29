namespace Blackjack
{
    public class Card
    {
        public uint Number;
        public byte Weight;

        public Card(uint number, byte weight)
        {
            this.Number = number;
            this.Weight = weight;
        }
    }
}
