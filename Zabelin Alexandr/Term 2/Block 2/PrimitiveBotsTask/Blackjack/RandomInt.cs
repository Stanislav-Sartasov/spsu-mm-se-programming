using System;

namespace Blackjack
{
    static internal class RandomInt
    {
        static internal int RandInt(int max)
        {
            var random = new Random(DateTime.Now.Millisecond);

            return random.Next(max);
        }
    }
}
