using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;

namespace Task2
{
    public static class MPISorter
    {
        private static int commSize;
        private static List<int> values = new List<int>();

        public static void Sort(ref List<int> list)
        {
            var comm = Communicator.world;
            commSize = comm.Size;

            if (!((commSize & (commSize - 1)) == 0)) // if n != 2^m ~ 4/8/16
            {
                Console.WriteLine($"-n should be degree of 2, not [{commSize}].");
                return;
            }

            // if list.Lenght % -n != 0, we should add +inf
            int additional = (commSize - list.Count % commSize) % commSize;

            for (int i = 0; i < additional; i++)
            {
                list.Add(int.MaxValue);
            }

            // distibuting list elements to all processes
            int blockSize = list.Count / commSize;

            for (int i = 0; i < blockSize; i++)
            {
                values.Add(list[blockSize * comm.Rank + i]);
            }


            for (int i = (int)Math.Log2(commSize); i > 0; i--)
            {
                // foreach group of processes shared base element.
                int groupsCount = 1 << ((int)Math.Log2(commSize) - i);
                List<int>[] groups;

                //forming groups + sending general
                if (comm.Rank == 0)
                {
                    groups = GetGroups(groupsCount);
                    for (int j = 1; j < commSize; j++)
                    {
                        comm.Send(groups, j, 0);
                    }
                }
                else
                {
                    groups = comm.Receive<List<int>[]>(0, 0);
                }

                // group membership index 
                int baseIndex = comm.Rank * groupsCount / commSize;

                // process group
                List<int> group = groups[baseIndex];
                int firstInGroup = group.First();

                // finding base element
                int baseElement;
                if (comm.Rank == firstInGroup)
                {
                    baseElement = FindBase();
                    foreach (var rank in group)
                    {
                        if (rank != firstInGroup)
                        {
                            comm.Send(baseElement, rank, 0);
                        }
                    }
                }
                else
                {
                    baseElement = comm.Receive<int>(firstInGroup, 0);
                }

                // forming pairs to compare and split (general info)
                Dictionary<int, List<int>> pairs;
                if (comm.Rank == 0)
                {
                    pairs = GeneratePairs(i);
                    for (int j = 1; j < commSize; j++)
                    {
                        comm.Send(pairs, j, 0);
                    }
                }
                else
                {
                    pairs = comm.Receive<Dictionary<int, List<int>>>(0, 0);
                }

                // simultaneously cmp and split
                for (int k = 0; k < comm.Size / 2; k++)
                {
                    if (comm.Rank == pairs[0][k] || comm.Rank == pairs[1][k])
                    {
                        CompareAndSplit(comm, pairs[0][k], pairs[1][k], baseElement);
                    }
                    // pairs are mutually exclusive
                }
            }

            List<int>[] sortedValues = comm.Gather(values, 0);
            if (comm.Rank == 0)
            {
                list = sortedValues.SelectMany(x => x).Take(list.Count - additional).ToList();
                for (int i = 1; i < commSize; i++)
                {
                    comm.Send(list, i, 0);
                }
            }
            else
            {
                list = comm.Receive<List<int>>(0, 0);
            }

            values.Clear();
        }

        public static Dictionary<int, List<int>> GeneratePairs(int pos)
        {
            var range = Enumerable.Range(0, commSize);
            return new Dictionary<int, List<int>>()
            {
                [0] = range.Where(x => TakeDigit(pos, x) == 0).ToList(),
                [1] = range.Where(x => TakeDigit(pos, x) == 1).ToList()
            };
        }

        // fractal dissection on 2 parts
        public static List<int>[] Вissection(List<int>[] array)
        {
            List<int>[] groups = new List<int>[2 * array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int asd = (int)Math.Log2(commSize / array.Length);
                groups[2 * i + 0] = array[i].Where(x => TakeDigit(asd, x) == 0).ToList();
                groups[2 * i + 1] = array[i].Where(x => TakeDigit(asd, x) == 1).ToList();
            }
            return groups;
        }

        public static List<int>[] GetGroups(int groupCount)
        {
            List<int>[] initGroup = { Enumerable.Range(0, commSize).ToList() };
            if (groupCount == 1)
                return initGroup;
            else
            {
                for (int i = 0; i < Math.Log2(groupCount); i++)
                {
                    initGroup = Вissection(initGroup);
                }
            }
            return initGroup;
        }

        public static int FindBase()
        {
            return values.Any() ? values[values.Count / 2] : 0;
        }

        private static void CompareAndSplit(Communicator comm, int rank1, int rank2, int baseElement)
        {

            if (comm.Rank == rank2)
            {
                comm.Send(values, rank1, 0);
                values = comm.Receive<List<int>>(rank1, 1);
            }
            else
            {
                var rank2values = comm.Receive<List<int>>(rank2, 0);
                values = values.Concat(rank2values).ToList();
                values.Sort();

                var greaterValues = values.Where(x => x > baseElement).ToList();
                comm.Send(greaterValues, rank2, 1);

                values = values.Where(x => x <= baseElement).ToList();
            }
        }

        // ____001110
        // _____N__21 N===pos

        public static int TakeDigit(int pos, int id)
        {
            var binId = Convert.ToString(id, 2);
            return pos > binId.Length ? 0 : binId[binId.Length - pos] - '0';
        }
    }
}
