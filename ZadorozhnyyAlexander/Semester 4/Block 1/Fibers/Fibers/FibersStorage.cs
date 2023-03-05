namespace Fibers
{
    public class FibersStorage : IDisposable
    {
        private readonly int maxPriority = 10;
        private Dictionary<int, List<FiberStructure>> fibersStorage;

        private int FindPriority(FiberStructure fiber) => fiber.Priority % maxPriority;

        private bool IsEmptyList(List<FiberStructure> lst) => lst.Count == 0;

        private void DropFiber()
        {
            if (!IsEmptyList(fibersStorage[-2]))
            {
                var tmp = fibersStorage[-2].First();
                fibersStorage[-2].Clear();
                fibersStorage[-1].Add(tmp);
                fibersStorage[FindPriority(tmp)].Add(tmp);
                
            }
        }

        private void HoldFiber(FiberStructure fiber)
        {
            DropFiber();
            if (!IsEmptyList(fibersStorage[-2]))
                return;

            fibersStorage[-2].Add(fiber);
            fibersStorage[-1].Remove(fiber);
            fibersStorage[FindPriority(fiber)].Remove(fiber);
        }

        public FibersStorage()
        {
            fibersStorage = new Dictionary<int, List<FiberStructure>>(maxPriority + 2);
            for (int i = -2; i < maxPriority; i++)
                fibersStorage.Add(i, new List<FiberStructure>());
        }

        public bool IsEmpty => IsEmptyList(fibersStorage[-1]);

        public void AddFiber(Process procces)
        {
            var fiber = new FiberStructure(procces.Priority, new Fiber(procces.Run));
            fibersStorage[-1].Add(fiber);
            fibersStorage[FindPriority(fiber)].Add(fiber);
        }

        public void RemoveFiber(FiberStructure current)
        {
            fibersStorage[-2].Clear();
            fibersStorage[-1].Remove(current);
            fibersStorage[FindPriority(current)].Remove(current);
        }

        public FiberStructure TakeFiber(Priorities priority)
        {
            FiberStructure data = null;

            if (IsEmpty)
                DropFiber();

            switch (priority)
            {
                case Priorities.NonPriority:
                    data = fibersStorage[-1].First();
                    break;

                case Priorities.Priority:
                    for (int i = maxPriority - 1; i >= 0; i--)
                    {
                        if (!IsEmptyList(fibersStorage[-2]) && fibersStorage[-2].First().Priority > i)
                        {
                            data = fibersStorage[-2].First();
                            break;
                        }
                             
                        if (fibersStorage[i].Count != 0)
                        {
                            data = fibersStorage[i].First();
                            break;
                        }
                    }
                    break;
            }

            if (data != null)
                HoldFiber(data);

            return data;
        }

        public void Dispose()
        {   
            if (!IsEmpty)
                foreach (var (_, lst) in fibersStorage)
                {
                    foreach (var fib in lst)
                        Fiber.Delete(fib.Fiber.Id);
                    lst.Clear();
                }
                    
            fibersStorage.Clear();
        }
    }
}
