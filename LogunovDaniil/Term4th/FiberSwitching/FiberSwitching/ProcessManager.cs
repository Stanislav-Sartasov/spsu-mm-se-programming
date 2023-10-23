using Fibers;
using FiberSchedulers;

namespace ProcessManager
{
	public static class ProcessManager
	{
		private static FiberRecord? _currentFiber;
		private static AFiberScheduler? _currentScheduler;

		public static void RunProcesses(List<Process> processes, AFiberScheduler scheduler)
		{
			_currentScheduler = scheduler;

			foreach (Process process in processes)
			{
				var fiber = new FiberRecord(new Fiber(process.Run), process.Priority);
				// starting from primary fiber if such was initiated here
				if (fiber.Fiber.IsPrimary)
				{
					_currentFiber = fiber;
					continue;
				}
				scheduler.QueueFiber(fiber);
			}

			if (_currentFiber == null) _currentFiber = scheduler.SelectNextFiber();
			if (_currentFiber == null) return; // no fibers to run

			Fiber.Switch(_currentFiber.Fiber.Id);
		}

		public static void Switch(bool fiberFinished)
		{
			if (_currentFiber == null) throw new InvalidOperationException("switching from null fiber!");
			if (_currentScheduler == null) throw new InvalidOperationException("switching without scheduler!");

			var previousFiber = _currentFiber;
			if (!fiberFinished)
			{
				_currentScheduler.QueueFiber(_currentFiber);
			}

			_currentFiber = _currentScheduler.SelectNextFiber();
			if (_currentFiber == null)
				Fiber.Switch(Fiber.PrimaryId);
			else
				Fiber.Switch(_currentFiber.Fiber.Id);

			if (fiberFinished)
			{
				Fiber.Delete(previousFiber.Fiber.Id);
			}
		}
	}
}
