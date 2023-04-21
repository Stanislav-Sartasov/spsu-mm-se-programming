using MPI;

namespace OddEvenSort;

public static class Sort
{
    private static T[] Merge<T>(T[] arr1, T[] arr2) where T : IComparable<T>
    {
        int i = 0, j = 0, k = 0;
        var merged = new T[arr1.Length + arr2.Length];

        while (i < arr1.Length && j < arr2.Length)
            if (arr1[i].CompareTo(arr2[j]) < 0)
                merged[k++] = arr1[i++];
            else
                merged[k++] = arr2[j++];

        while (i < arr1.Length) merged[k++] = arr1[i++];

        while (j < arr2.Length) merged[k++] = arr2[j++];

        return merged;
    }

    public static T[] OddEvenSort<T>(T[] list) where T : IComparable<T>
    {
        var comm = Communicator.world;
        var commRank = comm.Rank;
        var commSize = comm.Size;

        var subListLength = list.Length / commSize + (list.Length % commSize > 0 ? 1 : 0);
        var subList = new T[subListLength];

        if (commRank == 0)
            subList = comm.Scatter(list.Chunk(subListLength).ToArray(), 0);
        else
            subList = comm.Scatter<T[]>(0);

        Array.Sort(subList);

        for (var phase = 0; phase < commSize; phase++)
        {
            if (phase % 2 == commRank % 2 && commRank < commSize - 1)
            {
                comm.Send<T[]>(subList, commRank + 1, 0);
                subList = comm.Receive<T[]>(commRank + 1, 0);
            }
            else if (phase % 2 != commRank % 2 && commRank > 0)
            {
                var received = comm.Receive<T[]>(commRank - 1, 0);
                var merged = Merge(received, subList);

                comm.Send<T[]>(merged.Take(subListLength).ToArray(), commRank - 1, 0);
                subList = merged.Skip(subListLength).ToArray();
            }
        }

        if (commRank == 0)
            list = comm.Gather(subList, 0).SelectMany(x => x).ToArray();
        else
            comm.Gather(subList, 0);

        return list;
    }
}