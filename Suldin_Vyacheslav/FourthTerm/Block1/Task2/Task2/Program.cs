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

            //UnitTest();
            //return;

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
                    Console.WriteLine(list.Count);
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

        public static void UnitTest()
        {
            string[] args = Array.Empty<string>();

            ResourceManager rm = new ResourceManager("Task2.Properties.Resources", Assembly.GetExecutingAssembly());

            if (rm == null)
            {
                Console.WriteLine("ASDD");
            }

            using (MPI.Environment we = new MPI.Environment(ref args))
            {
                var comm = Communicator.world;

                for (int test = 1; test <= 3; test++)
                {
                    var list = new List<int>();
                  
                    if (comm.Rank == 0)
                    {
                        string testCase = System.Text.Encoding.ASCII.GetString((byte[])rm.GetObject("test_unsorted" + test.ToString()));

                        list = testCase.Split(" ").Select(x => Convert.ToInt32(x)).ToList();


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
                        string sorted = System.Text.Encoding.ASCII.GetString((byte[])rm.GetObject("test_sorted" + test.ToString()));
                        bool areEqual = sorted.Split(" ").Select(x => Convert.ToInt32(x)).SequenceEqual(list);
                        Console.WriteLine($"Test [{test}] is [{areEqual}]");
                    }
                }
            }            
        }
    }
}