namespace Blackjack
{
    internal class Card
    {
        internal byte Weight { get; private set; }

        internal Card(byte weight)
        {
            this.Weight = weight;
        }
    }
}