using NUnit.Framework;

namespace Task5.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            if ( 1== 1) Assert.Pass();
        }
    }
}