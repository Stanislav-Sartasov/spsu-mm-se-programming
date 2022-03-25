namespace Roulette;
using System.Security.Cryptography;

internal class RouletteWheel
{
    private readonly List<int> pockets;

    internal RouletteWheel()
    {
        int[] pocketsCopy =
            { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13,
            36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14,
            31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 };

        pockets = new List<int>(pocketsCopy);
    }

    internal int ThrowBall()
    {
        int nextIndex = RandomNumberGenerator.GetInt32(37);
        return pockets[nextIndex];
    }

    internal Parity GetParity(int number) => (Parity)(number % 2);

    internal Colour GetColour(int number) => (Colour)(pockets.IndexOf(number) % 2);

    internal Dozen GetDozen(int number) => (Dozen)((number - 1) / 12);
}
