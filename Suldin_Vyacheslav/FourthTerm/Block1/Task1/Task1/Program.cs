using ProcessManager;

namespace Task1
{
    public static class Program
    {
        public static void Main()
        {
            var processes = new List<Process>();
            for (int i = 0; i < 4; i++)
            {
                processes.Add(new Process());
            }
            ProcessManager.ProcessManager.Execute(processes, Priority.Classic);
        }
    }
}