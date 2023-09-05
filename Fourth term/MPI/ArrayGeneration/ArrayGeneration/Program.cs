using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Random r = new Random(DateTime.Now.Millisecond);

            int capacity = 100;

            List<int> lst = new List<int>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                lst.Add(r.Next(1000001));
            }

            File.WriteAllText("C:\\temp\\test_unsorted3.dat", string.Join(" ", lst.Select(x => x.ToString())));

            lst.Sort();

            File.WriteAllText("C:\\temp\\test_sorted3.dat", string.Join(" ", lst.Select(x => x.ToString())));
            sw.Stop();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
