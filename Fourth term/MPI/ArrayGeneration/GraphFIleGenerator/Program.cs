using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFileGenerator
{
    struct Edge
    {
        public int FirstVertice, SecondVertice;
        public int Weight;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FirstVertice, SecondVertice, Weight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int verticesCount = 7;

            int minEdges = 1000000;

            List<Edge> edges = //GenerateEdges(verticesCount, minEdges, verticesCount * verticesCount / 2);
                new List<Edge>()
                {
                    new Edge(){FirstVertice=2,SecondVertice=3,Weight=2},
                    new Edge(){FirstVertice=2,SecondVertice=4,Weight=1},
                    new Edge(){FirstVertice=2,SecondVertice=5,Weight=2},
                    new Edge(){FirstVertice=3,SecondVertice=4,Weight=2},
                    new Edge(){FirstVertice=3,SecondVertice=5,Weight=1},
                    new Edge(){FirstVertice=4,SecondVertice=5,Weight=2},
                    new Edge(){FirstVertice=5,SecondVertice=6,Weight=4},
                };

            GC.Collect();

            /*var lst = new List<string>(edges.Count +1);
            lst.Add(verticesCount.ToString());
            lst.AddRange(edges.Select(x => x.ToString()));
            File.WriteAllLines("C:\\temp\\graph1.dat", lst.ToArray());*/

            GenerateAnSavePathLengthMatrix(verticesCount, edges);

            //GenerateAnSaveMinimumTree(verticesCount, edges);
        }

        private static void GenerateAnSavePathLengthMatrix(int verticesCount, List<Edge> edges)
        {
            int[,] matrix = new int[verticesCount, verticesCount];

            List<int> indices = new List<int>();

            for (int i = 0; i < verticesCount; i++)
            {
                indices.Add(i);
                
                for (int j = 0; j < verticesCount; j++)
                {
                    matrix[i, j] = 1000000000;
                }
                matrix[i, i] = 0;
            }
            
            foreach (var e in edges)
            {
                matrix[e.FirstVertice, e.SecondVertice] = matrix[e.SecondVertice, e.FirstVertice] = e.Weight;
            }

            for(int i=0;i<verticesCount;i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    for (int k = 0; k < verticesCount; k++)
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i, k] + matrix[k, j]);
                    }
                }
            }

            File.WriteAllLines("C:\\temp\\Sultim\\test0_my.txt", indices.Select(i => string.Join(" ", indices.Select(j => matrix.GetValue(i, j).ToString()))));
        }

        private static void GenerateAnSaveMinimumTree(int verticesCount, List<Edge> edges)
        {
            List<Edge> result = new List<Edge>();

            Dictionary<int, int> hash = new Dictionary<int, int>();
            List<List<int>> buckets = new List<List<int>>();

            bool allVerticesAreReachable = false;

            edges.Sort((x, y) =>
            {
                if (x.Weight - y.Weight != 0)
                    return x.Weight - y.Weight;
                if (x.FirstVertice - y.FirstVertice != 0)
                    return x.FirstVertice - y.FirstVertice;
                return x.SecondVertice - y.SecondVertice;
            }
            );

            int j = 0;

            while (!allVerticesAreReachable)
            {
                Edge e = edges[j++];

                if (hash.ContainsKey(e.FirstVertice) && !hash.ContainsKey(e.SecondVertice))
                {
                    hash[e.SecondVertice] = hash[e.FirstVertice];
                    buckets[hash[e.FirstVertice]].Add(e.SecondVertice);
                    result.Add(e);
                }
                else if (!hash.ContainsKey(e.FirstVertice) && hash.ContainsKey(e.SecondVertice))
                {
                    hash[e.FirstVertice] = hash[e.SecondVertice];
                    buckets[hash[e.SecondVertice]].Add(e.FirstVertice);
                    result.Add(e);
                }
                else if (!hash.ContainsKey(e.FirstVertice) && !hash.ContainsKey(e.SecondVertice))
                {
                    buckets.Add(new List<int>() { e.FirstVertice, e.SecondVertice });
                    hash[e.FirstVertice] = hash[e.SecondVertice] = buckets.Count - 1;
                    result.Add(e);
                }
                else // Оба есть
                {
                    if (hash[e.FirstVertice] != hash[e.SecondVertice])
                    {
                        int targetBucket = Math.Min(hash[e.FirstVertice], hash[e.SecondVertice]);
                        int sourceBucket = Math.Max(hash[e.FirstVertice], hash[e.SecondVertice]);

                        foreach (int i in buckets[sourceBucket])
                        {
                            hash[i] = targetBucket;
                        }

                        buckets[targetBucket].AddRange(buckets[sourceBucket]);
                        buckets[sourceBucket] = null;
                        result.Add(e);
                    }
                }
                allVerticesAreReachable = (buckets.Count(x => x != null) == 1) && (buckets.First(x => x != null).Count == verticesCount);
            }

            var lst = new List<string>();
            lst.Add(verticesCount.ToString());
            lst.Add(result.Sum(x => x.Weight).ToString());
            File.WriteAllLines("C:\\temp\\minGraph1.dat", lst.ToArray());
        }

        private static List<Edge> GenerateEdges(int verticesCount, int minEdges, int maxEdges)
        {
            Random r = new Random(DateTime.Now.Millisecond);

            List<Edge> result = new List<Edge>();

            Dictionary<int, int> hash = new Dictionary<int, int>();
            List<List<int>> buckets = new List<List<int>>();

            bool allVerticesAreReachable = false;

            while(!allVerticesAreReachable)
            {
                Edge e = new Edge { FirstVertice = r.Next(0,verticesCount), SecondVertice = r.Next(0, verticesCount), Weight = r.Next(1, verticesCount) };

                if (e.FirstVertice == e.SecondVertice) continue; // без петель

                result.Add(e);

                if (hash.ContainsKey(e.FirstVertice) && !hash.ContainsKey(e.SecondVertice))
                {
                    hash[e.SecondVertice] = hash[e.FirstVertice];
                    buckets[hash[e.FirstVertice]].Add(e.SecondVertice);
                }
                else if (!hash.ContainsKey(e.FirstVertice) && hash.ContainsKey(e.SecondVertice))
                {
                    hash[e.FirstVertice] = hash[e.SecondVertice];
                    buckets[hash[e.SecondVertice]].Add(e.FirstVertice);
                }
                else if (!hash.ContainsKey(e.FirstVertice) && !hash.ContainsKey(e.SecondVertice))
                {
                    buckets.Add(new List<int>() { e.FirstVertice, e.SecondVertice });
                    hash[e.FirstVertice] = hash[e.SecondVertice] = buckets.Count - 1;
                }
                else // Оба есть
                {
                    if(hash[e.FirstVertice] != hash[e.SecondVertice])
                    {
                        int targetBucket = Math.Min(hash[e.FirstVertice], hash[e.SecondVertice]);
                        int sourceBucket = Math.Max(hash[e.FirstVertice], hash[e.SecondVertice]);

                        foreach(int i in buckets[sourceBucket])
                        {
                            hash[i] = targetBucket;
                        }

                        buckets[targetBucket].AddRange(buckets[sourceBucket]);
                        buckets[sourceBucket] = null;
                    }
                }

                allVerticesAreReachable = (buckets.Count(x => x != null) == 1) && (buckets.First(x => x != null).Count == verticesCount);
            }

            int targetEdgesNumber = r.Next(minEdges, maxEdges);

            while(result.Count < targetEdgesNumber)
            {
                Edge e = new Edge { FirstVertice = r.Next(0, verticesCount), SecondVertice = r.Next(0, verticesCount), Weight = r.Next(1, verticesCount) };

                if (e.FirstVertice == e.SecondVertice) continue;

                result.Add(e);
            }

            return result;
        }

    }
}
