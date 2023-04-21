using MPI;
using System.Linq;
using FileManager;

namespace Sort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var env = new MPI.Environment(ref args))
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Not enough args!");
                    return;
                }

                var comm = Communicator.world;
                var rank = comm.Rank;
                var size = comm.Size;

                var data = new List<int>();
                if (rank == 0)
                {
                    var d = FileManager.FileManager.FileToIntList(args[0], "\n");

                    if (FileManager.FileManager.IsReaded)
                    {
                        comm.Scatter(Enumerable.Repeat(true, size).ToArray(), 0);
                        data = d;
                    }
                    else
                    {
                        comm.Scatter(Enumerable.Repeat(false, size).ToArray(), 0);
                        Console.WriteLine("Unable to open file to read (((");
                        return;
                    }
                }
                else
                {
                    if (!comm.Scatter<bool>(0)) return;
                }

                for (var k = 0; k < size + 1; k++)
                {
                    var list = new List<int>();
                    if (rank == 0)
                    {
                        var sublists = new List<int>[size];
                        var skip = 0;

                        for (var i = 0; i < size; i++)
                        {
                            var take = (data.Count / size) + (data.Count % size > i ? 1 : 0);
                            sublists[i] = data
                                .Skip(skip)
                                .Take(take)
                                .ToList();
                            skip += take;
                        }

                        list = comm.Scatter(sublists.ToArray(), 0);
                    }
                    else
                        list = comm.Scatter<List<int>>(0);

                    list.Sort();

                    // --0-- --1-- --2-- --3-- --4-- --5--
                    //  rec   get   rec   get   rec   get
                    // ---    rec   get   rec   get   ---

                    // --0-- --1-- --2-- --3-- --4-- 
                    //  rec   get   rec   get   ---  
                    // ---    rec   get   rec   get

                    if (rank % 2 == k % 2)
                    {
                        if (rank + 1 < size)
                        {
                            comm.Send(list, rank + 1, 0);

                            list = comm.Receive<List<int>>(rank + 1, 0);
                        }
                    }
                    else
                    {
                        if (rank - 1 >= 0)
                        {
                            var recList = comm.Receive<List<int>>(rank - 1, 0);

                            list = list.Concat(recList).ToList();
                            list.Sort();

                            comm.Send(list.Take(list.Count / 2).ToList(), rank - 1, 0);
                            list = list.Skip(list.Count / 2).ToList();
                        }
                    }

                    if (rank == 0)
                        data = comm.Gather(list, 0).SelectMany(x => x).ToList();
                    else
                        comm.Gather(list, 0);
                }

                if (rank == 0)
                {
                    FileManager.FileManager.StringToFile(string.Join("\n", data), args[1]);
                    Console.WriteLine("Sorted");
                }
            }
        }
    }
}

