namespace Roulette;
using System.Security.Cryptography;

public class RouletteWheel
{
    private readonly List<int> pockets;

    public RouletteWheel()
    {
        int[] pocketsCopy =
            { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13,
            36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14,
            31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 };

        pockets = new List<int>(pocketsCopy);
    }

    public int ThrowBall()
    {
        int nextIndex = RandomNumberGenerator.GetInt32(37);
        return pockets[nextIndex];
    }

    public Parity GetParity(int number) => (Parity)(number % 2);

    public Colour GetColour(int number) => (Colour)(pockets.IndexOf(number) % 2);

    public Dozen GetDozen(int number) => (Dozen)((number - 1) / 12);
}
