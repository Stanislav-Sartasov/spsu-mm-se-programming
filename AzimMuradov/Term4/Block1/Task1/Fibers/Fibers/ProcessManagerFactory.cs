namespace Fibers;

public static class ProcessManagerFactory
{
    public static ProcessManager Create(ProcessManagerStrategy strategy)
    {
        switch (strategy)
        {
            case ProcessManagerStrategy.RoundRobin:
                var cacheR = new List<uint>();
                return new ProcessManager(processesData =>
                {
                    var processesIds = processesData.Select(pd => pd.Id).ToList();
                    var candidates = processesIds.Where(id => !cacheR.Contains(id)).ToList();

                    uint id;
                    if (!candidates.Any())
                    {
                        cacheR.Clear();
                        id = processesIds.First();
                    }
                    else
                    {
                        id = candidates.First();
                    }

                    cacheR.Add(id);

                    return id;
                });
            case ProcessManagerStrategy.Prioritized:
                var cacheP = new List<uint>();
                return new ProcessManager(processesData =>
                {
                    var max = processesData.Max(pd => pd.Priority);
                    var processesIds = processesData
                        .Where(pd => pd.Priority == max)
                        .Select(pd => pd.Id)
                        .ToList();
                    var candidates = processesIds.Where(id => !cacheP.Contains(id)).ToList();

                    uint id;
                    if (!candidates.Any())
                    {
                        cacheP.Clear();
                        id = processesIds.First();
                    }
                    else
                    {
                        id = candidates.First();
                    }

                    cacheP.Add(id);

                    return id;
                });
            default:
                throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null);
        }
    }
}