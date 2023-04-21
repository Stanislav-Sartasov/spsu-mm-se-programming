using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace Fibers
{
	public static class ProcessManager
	{
		private static readonly List<Fiber> Fibers = new List<Fiber>();
		private static bool _isPrioMode;
		private static Fiber _current;
		private static Random _rnd = new Random();

		public static void Start(List<Process> processes, bool prioMode)
		{
			foreach (var process in processes)
				Fibers.Add(new Fiber(process.Run, process.TotalDuration, process.Priority));

			_isPrioMode = prioMode;

			_current = Fibers.First();
			Fiber.Switch(_current.Id);
		}

		public static void Switch(bool fiberFinished)
		{
			if (fiberFinished)
			{
				Console.WriteLine($"Fiber [{_current.Id}] has finished");
				Fibers.Remove(_current);
			}

			if (Fibers.Count == 0)
			{
				Console.WriteLine("Switching to primary fiber");
				Fiber.Switch(Fiber.PrimaryId);
			}

			if (_isPrioMode)
			{
				if (_rnd.Next(100) < 25)
				{
					_current = Fibers[_rnd.Next(Fibers.Count)];
				}
				else
				{
					int maxPriority = Fibers.Aggregate(Fibers[0].Priority, (acc, c) => Math.Max(acc, c.Priority));

					var prioFibers = Fibers.FindAll(fiber => fiber.Priority == maxPriority);

					// Get fiber with max priority and min duration
					_current = prioFibers.Aggregate(prioFibers.First(),
						(acc, c) => c.TotalDuration < acc.TotalDuration ? c : acc);
				}
			}
			else
			{
				int index = Fibers.IndexOf(_current);
				_current = Fibers[(index + 1) % Fibers.Count];
			}

			Console.WriteLine($"Switching to [{_current.Id}] fiber");
			Fiber.Switch(_current.Id);
		}
	}
}