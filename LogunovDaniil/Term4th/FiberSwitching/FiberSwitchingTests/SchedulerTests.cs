using FiberSchedulers;

namespace FiberSwitchingTests
{
	public class SchedulerTests
	{
		private void DoNothing() { }

		[Test]
		public void NonPrioritisedSchedulerTest()
		{
			var scheduler = new NonPrioritisedScheduler();

			scheduler.QueueFiber(new FiberRecord(new Fibers.Fiber(DoNothing), 5));
			scheduler.QueueFiber(new FiberRecord(new Fibers.Fiber(DoNothing), 6));

			var fiber = scheduler.SelectNextFiber();
			Assert.IsNotNull(fiber);
			Assert.That(fiber.Priority, Is.EqualTo(5));

			fiber = scheduler.SelectNextFiber();
			Assert.IsNotNull(fiber);
			Assert.That(fiber.Priority, Is.EqualTo(6));
		}

		[Test]
		public void NonPrioritisedCheckNullTest()
		{
			var scheduler = new NonPrioritisedScheduler();

			var fiber = scheduler.SelectNextFiber();

			Assert.IsNull(fiber);
		}

		[Test]
		public void PrioritisedSchedulerTest()
		{
			var scheduler = new PrioritisedScheduler();
			var fibers = 50;

			for (int i = 0; i < fibers; i++)
			{
				scheduler.QueueFiber(new FiberRecord(new Fibers.Fiber(DoNothing), 5));
			}
			for (int i = 0; i < fibers; i++)
			{
				Assert.IsNotNull(scheduler.SelectNextFiber());
			}
			Assert.IsNull(scheduler.SelectNextFiber());
		}

		[Test]
		public void PrioritisedCheckNullTest()
		{
			var scheduler = new PrioritisedScheduler();

			var fiber = scheduler.SelectNextFiber();

			Assert.IsNull(fiber);
		}
	}
}