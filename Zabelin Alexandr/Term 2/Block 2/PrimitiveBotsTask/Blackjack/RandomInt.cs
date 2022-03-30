using System;

namespace Blackjack
{
    static internal class RandomInt
    {
        static internal int RandInt(int max)
        {
            var random = new Random();

            return random.Next(max);
        }
    }
}
