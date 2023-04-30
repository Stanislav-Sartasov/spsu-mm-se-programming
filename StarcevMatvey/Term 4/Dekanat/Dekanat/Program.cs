using Dekanat.DekanatLib;

namespace Dekanat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var table = new StripedHashSet(4);

            table.Add(1, 1);

            Console.WriteLine(table.Count());

            table.Remove(1, 1);

            Console.WriteLine(table.Count());

        }
    }
}