using ConsumerProducer;
using NUnit.Framework;

namespace ConsumerProducerTests
{
    public class UtilsTests
    {
        [Test]
        public void GetPositiveIntTest()
        {
            var rez = Utils.GetPositiveInt("1");
            Assert.AreEqual(rez, 1);

            rez = Utils.GetPositiveInt("-1");
            Assert.AreEqual(rez, 0);

            rez = Utils.GetPositiveInt("Never gonna let you down, ...");
            Assert.AreEqual(rez, 0);
        }
    }
}