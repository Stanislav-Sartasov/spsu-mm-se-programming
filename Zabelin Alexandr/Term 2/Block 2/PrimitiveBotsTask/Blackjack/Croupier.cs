using System;

namespace Blackjack
{
    internal class Croupier
    {
        internal byte Score { get; private set; }
        internal byte VisibleCardWeight { get; private set; }

        internal void GetCardWeight(byte weight)
        {
            if (weight == 11 && this.Score > 10)
            {
                this.Score += 1;
            }
            else
            {
                this.Score += weight;
            }
        }

        internal void GetInitCardWeight(byte weight)
        {
            this.Score = weight;
            this.VisibleCardWeight = weight;
        }
    }
}
