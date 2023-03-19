using System.Text.RegularExpressions;

namespace MPIFloydWarshallAlgorithm;

public static partial class GraphFileManager
{
    [GeneratedRegex("\\d+$", RegexOptions.Compiled)]
    private static partial Regex PositiveRegex();

    [GeneratedRegex("(?<vertexIndex1>\\d+) (?<vertexIndex2>\\d+) (?<weight>\\d+)$", RegexOptions.Compiled)]
    private static partial Regex EdgeRegex();

    public static int[] ReadGraph(string pathToFile, out int vertexNumber)
    {
        var positiveRegex = PositiveRegex();
        var edgeRegex = EdgeRegex();

        using var reader = new StreamReader(pathToFile);
        string? line;

        if ((line = reader.ReadLine()) == null)
        {
            throw new InvalidDataException("File is empty");
        }

        var matched = positiveRegex.Match(line);

        if (!matched.Success)
        {
            throw new InvalidDataException("First line of the file must contain positive number");
        }

        vertexNumber = int.Parse(matched.Value);

        var adjacencyMatrix = new int[vertexNumber * vertexNumber];

        for (int i = 0; i < vertexNumber; i++)
        {
            for (int j = 0; j < vertexNumber; j++)
            {
                adjacencyMatrix[i * vertexNumber + j] = i != j ? -1 : 0;
            }
        }

        while ((line = reader.ReadLine()) != null)
        {
            matched = edgeRegex.Match(line);

            if (!matched.Success)
            {
                throw new InvalidDataException("The file contains a line with a bad format");
            }

            var vertex1 = int.Parse(matched.Groups["vertexIndex1"].Value);
            var vertex2 = int.Parse(matched.Groups["vertexIndex2"].Value);
            var weight = int.Parse(matched.Groups["weight"].Value);

            if (vertex1 != vertex2)
            {
                adjacencyMatrix[vertex1 * vertexNumber + vertex2] = weight;
                adjacencyMatrix[vertex2 * vertexNumber + vertex1] = weight;
            }
        }

        return adjacencyMatrix;
    }

    public static void WriteMatrix(string pathToFile, int[] pathMatrix, int vertexNumber)
    {
        using var writer = new StreamWriter(pathToFile);

        for (int i = 0; i < vertexNumber; i++)
        {
            for (int j = 0; j < vertexNumber; j++)
            {
                writer.Write(pathMatrix[i * vertexNumber + j]);

                if (j != vertexNumber - 1)
                {
                    writer.Write(" ");
                }
            }
            writer.WriteLine();
        }
    }
}
