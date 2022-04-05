using System;

namespace Blackjack
{
    internal class Croupier
    {
        internal byte Score { get; private set; }
        internal byte VisibleCardWeight { get; private set; }

        internal void IncreaseScore(byte weight)
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

        internal void InitSetScore(byte weight)
        {
            this.Score = weight;
            this.VisibleCardWeight = weight;
        }

        internal bool IsBlackjack() => this.Score == 21;

        internal bool IsBust() => this.Score > 21;
    }
}
