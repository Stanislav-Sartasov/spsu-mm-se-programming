using MPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Resources;

namespace Task2
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Missing args for [i] or/and [out] files.");
                return;
            }

            List<int> list = new List<int>();

            using (MPI.Environment we = new MPI.Environment(ref args))
            {
                var comm = Communicator.world;

                if (comm.Rank == 0)
                {
                    using (var sr = new StreamReader(args[0]))
                    {
                        list = sr.ReadToEnd().Split(" ").Select(x => Convert.ToInt32(x)).ToList();
                    }
                    
                    for (int i = 1; i < comm.Size; i++)
                    {
                        comm.Send(list, i, 0);
                    }
                }
                else
                {
                    list = comm.Receive<List<int>>(0, 0);
                }

                MPISorter.Sort(ref list);

                if (comm.Rank == 0)
                {
                    using (var sw = new StreamWriter(args[1]))
                    {
                        sw.Write(String.Join(" ", list));
                    }
                }
            }
        }

    }
}