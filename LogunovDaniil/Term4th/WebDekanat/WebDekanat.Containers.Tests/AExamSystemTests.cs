namespace WebDekanat.Containers.Tests
{
	public abstract class AExamSystemTests
	{
		protected abstract IExamSystem GetNewSet();

		[Test]
		public void AddThreadedTest()
		{
			var set = GetNewSet();

			var threads = new List<Thread>();
			for (int i = 1; i <= 5; i++)
			{
				var start = Math.Min(0, i - 2);
				var end = Math.Min(100, i * 20 + 2);
				threads.Add(new Thread(() =>
				{
					for (int j = start; j < end; j++)
						set.Add(j, j);
				}));
			}

			threads.ForEach(t => t.Start());

			threads.ForEach(t => t.Join());

			for (int i = 0; i < 100; i++)
				Assert.IsTrue(set.Contains(i, i));
		}

		[Test]
		public void CountTest()
		{
			var set = GetNewSet();
			for (int i = 0; i < 100; i++)
				set.Add(i, i);
			Assert.That(set.Count, Is.EqualTo(100));
		}

		[Test]
		public void RemoveTest()
		{
			var set = GetNewSet();
			for (int i = 0; i < 100; i++)
				set.Add(i, i);
			set.Remove(50, 50);
			Assert.That(set.Count, Is.EqualTo(99));
			Assert.IsFalse(set.Contains(50, 50));
		}

		[Test]
		public void AddNoRepetitionTest()
		{
			var set = GetNewSet();

			for (int i = 0; i < 100; i++)
				set.Add(i, i);
			for (int i = 10; i < 20; i++)
				set.Add(i, i);

			for (int i = 0; i < 100; i++)
				Assert.IsTrue(set.Contains(i, i));
			Assert.That(set.Count, Is.EqualTo(100));
		}
	}
}