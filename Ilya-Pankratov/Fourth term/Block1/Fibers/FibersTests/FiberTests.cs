using Fibers.Fibers;

namespace FibersTests
{
    public class FiberTests
    {
        private Fiber normalFiber;

        [SetUp]
        public void Setup()
        {
            normalFiber = new Fiber(SwitchToPrimary);
        }

        [Test]
        public void SwitchTest()
        {
            Fiber.Switch(normalFiber.Id);
            Assert.False(normalFiber.IsPrimary);
            Fiber.Delete(normalFiber.Id);
            Assert.Pass();
        }

        private static void SwitchToPrimary()
        {
            Fiber.Switch(Fiber.PrimaryId);
        }
    }
}
