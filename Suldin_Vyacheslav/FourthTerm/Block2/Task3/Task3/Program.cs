namespace Task3;

public class Program
{
    public const int nC = 3;
    public const int nP = 3;

    public static int Main(string[] args)
    {
        List<int> workingOn = new List<int>();

        var manager = new Manager<int>(nC, nP, workingOn);

        manager.Manage();
        Console.ReadKey();
        manager.ShoutDown();

        return 0;
    }

}