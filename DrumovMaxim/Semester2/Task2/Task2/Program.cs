using Bots;

namespace Task2
{
    public class  Program
    {
        public static void Main()
        {
            Console.WriteLine("This program shows the implementation of the Casino game European Roulette: ");
            StartCasino casino = new StartCasino(new ThomasDonaldBot("ThomasDonaldBot", 30000, 40), "TylerDerden");
            casino.Launch();
            casino = new StartCasino(new WideStrideBot("WideStrideBot", 10000, 40), "PatrickBateman");
            casino.Launch();
            casino = new StartCasino(new ProgressionSeriesBot("ProgressionSeriesBot", 10000, 40), "ElliotAlderson");
            casino.Launch();
        }
    }
}