using System;
using System.Collections.Immutable;
using System.Globalization;
using MPI;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This program takes path to the file that contains integer array
            // and sorts it using parallel algorithm of quick sort
            using (var environment = new MPI.Environment(ref args))
            {
                var comm = Communicator.world;
                var size = comm.Size;
                var rank = comm.Rank;

                int[] scatteredInput;
                List<int> patternPart = new List<int>();
                string outputFile = "";
                if (rank == 0)
                {
                    // main process
                    if (args.Length != 2)
                    {
                        Console.WriteLine("Invalid number of input parameters. 2 filenames expected.");
                        return;
                    }

                    var inputFile = "../../../" + args[0];
                    outputFile = "../../../" + args[1];
                    if (!File.Exists(inputFile))
                    {
                        Console.WriteLine("File {0} does not exist.", inputFile);
                        return;
                    }

                    string content = File.ReadAllText(inputFile);
                    string[] strArr = content.Split(' ');
                    // splittedArs is used to give each process except 0 process its array
                    List<int>[] splittedListsArr = new List<int>[size];
                    int oneArrSize = strArr.Length / (size - 1);
                    for (int i = 0; i < splittedListsArr.Length; i++)
                    {
                        splittedListsArr[i] = new List<int>();
                    }
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if ((i / oneArrSize) + 1 >= 4)
                            splittedListsArr[(i / oneArrSize)].Add(Int32.Parse(strArr[i]));
                        else
                            splittedListsArr[(i / oneArrSize) + 1].Add(Int32.Parse(strArr[i]));
                    }
                    // Convert splittedListsArr into array of arrays
                    int[][] splittedArrays = new int[size][];
                    for (int i = 0; i < splittedArrays.Length; i++)
                    {
                        splittedArrays[i] = splittedListsArr[i].ToArray();
                    }
                    // Sending every not main process its array
                    scatteredInput = comm.Scatter(splittedArrays, 0);
                    for (int i = 1; i < size; i++)
                    {
                        comm.Send(strArr.Length, i, 0);
                    }

                }

                else
                {
                    scatteredInput = comm.Scatter<int[]>(0);
                    // Size of initial array
                    int receivedSize = comm.Receive<int>(0, 0);
                    // Sorting the array of each process excpet main process
                    Array.Sort(scatteredInput);
                    // Creating pattern.
                    // Note: we only give (size - 1) processes their arrays because 0 process is main process
                    int m = receivedSize / (int)(Math.Pow(size - 1, 2));
                    for (int i = 0; i < scatteredInput.Length; i += m)
                    {
                        patternPart.Add(scatteredInput[i]);
                    }
                }

                // Just ideal len of each process array after splitting by pattern.
                // Used only in creation unused array of main process.
                int splittedScatteredArrLen;
                // Array of small patterns. It is used to make one big pattern.
                int[][] splittedPattern = comm.Gather(patternPart.ToArray(), 0);
                // Array of each process splitted by big pattern.
                int[][] splittedScatteredArr;
                if (rank == 0)
                {
                    List<int> pattern = splittedPattern.SelectMany(x => x).ToList();
                    pattern.Sort();
                    List<int> boundaries = new List<int>();

                    for (int i = (size - 1) + ((size - 1) / 2) - 1;
                         i <= (size - 2) * (size - 1) + ((size - 1) / 2) - 1;
                         i += (size - 1))
                    {
                        boundaries.Add(pattern[i]);
                    }

                    for (int i = 1; i < size; i++)
                    {
                        comm.Send(boundaries, i, 0);
                    }

                    splittedScatteredArrLen = boundaries.Count + 1;
                    splittedScatteredArr = new int[splittedScatteredArrLen][];
                    
                }
                else
                {
                    // List of boundaries made out of pattern.
                    List<int> boundaries = comm.Receive<List<int>>(0, 0);
                    int currBoundary = boundaries[0];
                    int k = 0;
                    int left = 0;
                    int right = 0;
                    // Splitting array of each process by boundaries
                    List<System.ArraySegment<int>> splittedScatteredList = new List<System.ArraySegment<int>>();
                    for (int i = 0; i < scatteredInput.Length; i++)
                    {
                        if (scatteredInput[i] <= currBoundary)
                            right += 1;
                        if (scatteredInput[i] > currBoundary || i == scatteredInput.Length - 1)
                        {
                            splittedScatteredList.Add(new System.ArraySegment<int>(scatteredInput, left,
                                k == 0 ? right - left : right - left + 1));
                            left = i;
                            right = i;
                            k++;
                            if (k >= boundaries.Count)
                                currBoundary = Int32.MaxValue;
                            else
                                currBoundary = boundaries[k];
                        }
                    }

                    // Converting splittedScatteredList to array and gathering it into one big array of all processes.
                    splittedScatteredArr = new int[splittedScatteredList.Count][];

                    for (int i = 0; i < splittedScatteredList.Count; i++)
                    {
                        splittedScatteredArr[i] = splittedScatteredList[i].ToArray();
                    }
                }
                // Initial array, splitted by processes and boundaries.
                int[][][] fullSplittedArr = comm.Gather(splittedScatteredArr, 0);
                
                int[]?[] rightSplittedArr = new int[size][];
                int[] scatteredAnswer;
                if (rank == 0)
                {
                    // Joining fullSplittedArr into group by boundaries.
                    // First group has each processes array slice that has elements
                    // That are less than first boundary and so on...
                    for (int i = 1; i < fullSplittedArr.Length; i++)
                    {
                        for (int k = 0; k < fullSplittedArr[i].Length; k++)
                        {
                            if (i == 1 || rightSplittedArr[k + 1] == null)
                            {
                                rightSplittedArr[k + 1] = fullSplittedArr[i][k];
                            }
                            else
                                rightSplittedArr[k + 1] = rightSplittedArr[k + 1].Concat(fullSplittedArr[i][k]).ToArray();
                        }
                    }

                    // Future answer splitted in (size) arrays
                    scatteredAnswer = comm.Scatter(rightSplittedArr, 0);
                }
                else
                {
                    // Sorting array of each process except 0 process
                    scatteredAnswer = comm.Scatter<int[]>( 0);
                    Array.Sort(scatteredAnswer);

                }

                // Gathered scatteredAnswer arrays from each process
                int[][] almostAnswer = comm.Gather(scatteredAnswer, 0);
                if (rank == 0)
                {
                    // Creating answer and writing it in file.
                    string[] answer = almostAnswer[1].Select(x => x.ToString()).ToArray();

                    for (int i = 2; i < almostAnswer.Length; i++)
                    {
                        answer = answer.Concat(almostAnswer[i].Select(x => x.ToString()).ToArray()).ToArray();
                    }

                    string strAnswer = String.Join(" ", answer);
                    StreamWriter writer = new StreamWriter(outputFile);
                    writer.WriteLine(strAnswer);
                    writer.Close();
                }
            }

            Console.WriteLine("Sorted array was written to the output file.");
            
        }
    }
}