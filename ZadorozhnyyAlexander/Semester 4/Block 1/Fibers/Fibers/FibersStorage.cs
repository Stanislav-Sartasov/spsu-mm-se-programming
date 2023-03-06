namespace Fibers
{
    public class FibersStorage : IDisposable
    {
        private readonly int maxPriority = 10;
        private Dictionary<int, List<FiberStructure>> fibersStorage;

        private Random priorRnd = new Random();

        private int FindPriority(FiberStructure fiber) => fiber.Priority % maxPriority;

        private bool IsEmptyList(List<FiberStructure> lst) => lst.Count == 0;

        public FibersStorage()
        {
            fibersStorage = new Dictionary<int, List<FiberStructure>>(maxPriority + 1);
            for (int i = -1; i < maxPriority; i++)
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
            fibersStorage[-1].Remove(current);
            fibersStorage[FindPriority(current)].Remove(current);
        }

        public int RandomPriorityWithWeightedProbabilities()
        {
            // Calculate total weight (sum of weights from 2^1 to 2^10)
            int totalWeight = 2046;
            

            // Generate random number with weighted probabilities
            int randomNumber = 0;
            int randomWeight = priorRnd.Next(totalWeight);
            int weightSum = 0;

            for (int i = 0; i < 10; i++)
            {
                weightSum += 1<<i + 1;

                if (randomWeight < weightSum)
                {
                    randomNumber = i;
                    break;
                }
            }

            for (int i = randomNumber; i < 10; i++)
            {
                if (!IsEmptyList(fibersStorage[i]))
                    return i;
            }

            for (int i = randomNumber - 1; i >= 0; i--)
            {
                if (!IsEmptyList(fibersStorage[i]))
                    return i;
            }

            return randomNumber;
        }

        public FiberStructure TakeFiber(Priorities priority)
        {
            if (IsEmpty)
                return null;

            switch (priority)
            {
                case Priorities.NonPriority:
                    return fibersStorage[-1].First();

                case Priorities.Priority:
                    var prioretizedElements = fibersStorage[RandomPriorityWithWeightedProbabilities()];
                    var index = priorRnd.Next(prioretizedElements.Count);
                    return prioretizedElements[index];
            }

            return null;
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
