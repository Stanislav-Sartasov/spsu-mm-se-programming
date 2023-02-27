namespace Fibers.App;

public static class App
{
    public static void Main()
    {
        Console.WriteLine("Starting...");

        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.Prioritized))
        {
            for (var i = 0; i < 7; i++)
            {
                pm.AddTask(new Process(pm));
            }

            Console.WriteLine("Running...");

            pm.Run();

            Console.WriteLine("Finishing...");
        }

        Console.WriteLine("Finished");
    }
}