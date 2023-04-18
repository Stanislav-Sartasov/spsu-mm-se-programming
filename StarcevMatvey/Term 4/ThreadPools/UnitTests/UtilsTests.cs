using ThreadPool;
using NUnit.Framework;

namespace ThreadPooltests
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