using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayFileComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count()!=2)
            {
                Console.WriteLine("Invalid number of input parameters. 2 filenames expected.");
                return;
            }

            var file1 = args[0];
            var file2 = args[1];

            if(!File.Exists(file1))
            {
                Console.WriteLine(string.Format("File {0} does not exist.", file1));
                return;
            }

            if (!File.Exists(file2))
            {
                Console.WriteLine(string.Format("File {0} does not exist.", file2));
                return;
            }

            string contents1 = File.ReadAllText(file1);
            string contents2 = File.ReadAllText(file2);

            if(contents1.Length != contents2.Length)
            {
                Console.WriteLine("File sizes are different");
                return;
            }

            for(int i=0; i<contents1.Length;i++)
            {
                if(contents1[i] != contents2[i])
                {
                    Console.WriteLine(string.Format("Files are different at position {0}", i));
                    return;
                }
            }

            Console.WriteLine("Files are the same");
        }
    }
}
