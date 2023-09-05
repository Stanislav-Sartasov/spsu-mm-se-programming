using Fibers.Fibers;

namespace Fibers.ProcessManager;

public static class ProcessManager
{
	private static readonly List<IFiber> Processes = new();
	private static readonly List<IFiber>[] PrioritizedProcesses =
		new List<IFiber>[Process.PriorityLevelsNumber].Select(_ => new List<IFiber>()).ToArray();
	private static readonly Random Rng = new();

	private static IFiber? _current;
	private static AlgorithmType _usedAlgorithm;

	public static void Run(List<Process> processes, AlgorithmType usedStrategy)
	{
		RunFibers(processes.Select(process => (IFiber)new FiberWrapper(process)).ToList(), usedStrategy);
	}

	public static void RunFibers(List<IFiber> fiberWrappers, AlgorithmType usedStrategy)
	{
		_usedAlgorithm = usedStrategy;

		foreach (var fiber in fiberWrappers)
			if (usedStrategy == AlgorithmType.NoPriority)
				Processes.Add(fiber);
			else
				PrioritizedProcesses[fiber.Priority].Add(fiber);

		_current = SelectNext();
		_current.Switch();
	}

	public static void Switch(bool fiberFinished)
	{
		if (_current is null)
			throw new Exception("Trying to switch non-existing fiber");

		if (fiberFinished)
		{
			Processes.Remove(_current);
			PrioritizedProcesses[_current.Priority].Remove(_current);
		}

		if (Processes.Count == 0 && GetUnfinishedPriorityGroup() == -1)
		{
			_current.SwitchToPrimary();
		}
		else
		{
			_current = SelectNext();
			_current.Switch();
		}
	}

	private static int GetUnfinishedPriorityGroup()
	{
		for (var i = Process.PriorityLevelsNumber - 1; i >= 0; i--)
			if (PrioritizedProcesses[i].Count > 0)
				return i;
		return -1;
	}

	private static IFiber SelectNext()
	{
		switch (_usedAlgorithm)
		{
			case AlgorithmType.WithPriority:
			{
				var probClass = GenProbClass();
				return PrioritizedProcesses[probClass][Rng.Next(PrioritizedProcesses[probClass].Count)];
			}
			case AlgorithmType.NoPriority:
			{
				var tmp = Processes[0];
				Processes.RemoveAt(0);
				Processes.Add(tmp);
				GC.Collect();
				return Processes[0];
			}
			default:
				throw new NotImplementedException("This algorithm type is to be implemented");
		}
	}

	// Generates a number from Process.PriorityLevelsNumber to 0 with descending probability
	private static int GenProbClass()
	{
		for (var i = 1; i < Process.PriorityLevelsNumber; i++)
		{
			if (PrioritizedProcesses[Process.PriorityLevelsNumber - i].Count == 0)
				continue;

			if (Rng.NextSingle() < 0.8f)
				return Process.PriorityLevelsNumber - i;
		}

		return GetUnfinishedPriorityGroup();
	}
}