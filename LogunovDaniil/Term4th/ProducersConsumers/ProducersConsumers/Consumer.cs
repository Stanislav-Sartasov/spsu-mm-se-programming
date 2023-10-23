using ProducersConsumers.Interfaces;

namespace ProducersConsumers
{
	public class Consumer : AThreadedListAccessor
	{
		public Consumer(ILock locker, List<int> list) : base(locker, list)
		{
		}

		protected override void Work()
		{
			if (data.Count == 0)
			{
				Console.WriteLine($"Consumer #{id}: empty data list.");
				return;
			}
			var taken = data[0];
			data.RemoveAt(0);
			Console.WriteLine($"Consumer #{id}  took  \"{taken}\" from the data list.");
		}
	}
}
