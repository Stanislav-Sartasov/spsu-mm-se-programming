using ProducersConsumers.Interfaces;

namespace ProducersConsumers
{
	public class Producer : AThreadedListAccessor
	{
		private Random _rnd = new Random();

		public Producer(ILock locker, List<int> list) : base(locker, list)
		{
		}

		protected override void Work()
		{
			var put = _rnd.Next(10000, 99999);
			data.Add(put);
			Console.WriteLine($"Producer #{id} placed \"{put}\" to the data list.");
		}
	}
}
