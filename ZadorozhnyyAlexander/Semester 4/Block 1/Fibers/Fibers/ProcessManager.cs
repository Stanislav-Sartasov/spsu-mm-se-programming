namespace Fibers
{
    public class ProcessManager : IDisposable
    {
        private FibersStorage fibersStorage;
        private FiberStructure current;
        private Priorities priority;
        private bool isRunning;

        public ProcessManager(Priorities prior)
        {
            fibersStorage = new FibersStorage();
            priority = prior;
            isRunning = false;
            current = null;
        }

        public bool CheckProcess()
        {
            if (isRunning)
                Console.WriteLine("Process already running");
            else if (fibersStorage.IsEmpty)
                Console.WriteLine("No process to run");
            else
                return true;
            return false;
        }

        public void AddProcess(Process process) => fibersStorage.AddFiber(process);

        public void SwitchProcess(bool isDone)
        {
            if (isDone)
            {
                Console.WriteLine($"Current fiber {current.Fiber.Id} with priority {current.Priority} is finished");

                fibersStorage.RemoveFiber(current);

                if (fibersStorage.IsEmpty)
                {
                    Console.WriteLine($"Primary fiber {Fiber.PrimaryId} is finished");
                    Fiber.Switch(Fiber.PrimaryId);
                    return;
                }
            }

            if (!fibersStorage.IsEmpty)
                current = fibersStorage.TakeFiber(priority);

            if (current == null)
                throw new Exception("TakeFiber returned null!");
            
            Fiber.Switch(current.Fiber.Id);
        }

        public void RunProcess()
        {
            if (CheckProcess())
            {
                isRunning = true;
                SwitchProcess(false);
            }
                
        }

        public void Dispose() 
        {
            fibersStorage.Dispose();
            current = null;
            isRunning = false;
        }
    }
}
