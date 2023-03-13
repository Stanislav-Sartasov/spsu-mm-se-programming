using NUnit.Framework;
using MPI;
using System.Collections.Generic;
using Task2;
using System.Reflection;
using System.Resources;

namespace Task2.UnitTests
{
    public class Tests
    {
        [Test]
        public void MPISorterTest()
        {
            string[] args = { };

            ResourceManager rm = new ResourceManager("Task2.UnitTests.Properties.test_samples", Assembly.GetExecutingAssembly());

            for (int test = 1; test < 3; test++)
            {
                var list = new List<int>();

                using (MPI.Environment we = new MPI.Environment(ref args))
                {
                    var comm = Communicator.world;

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
                        Assert.AreEqual(sorted.Split(" ").Select(x => Convert.ToInt32(x)).ToList(), list);
                    }
                }
            }
        }
    }
}