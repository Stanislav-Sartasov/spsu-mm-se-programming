namespace Fibers
{
    public static class Program
    {
        public static void Main()
        {
            var pm = new ProcessManager(Priorities.Priority);
            for (int i = 0; i < 5; i++)
            {
                Process process = new Process(pm);
                pm.AddProcess(process);
            }

            pm.RunProcess();
            pm.Dispose();
            Console.WriteLine("End of program");

            Console.ReadKey();
        }
    }
}