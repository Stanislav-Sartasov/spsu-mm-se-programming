using Bash.Commands;
using NUnit.Framework;

namespace BashTests
{
    public class CommandsTests
    {
        [Test]
        public void CatGoodArgsTest()
        {
            var expected = "231 5opsaf\r\nfsdhjf\r\nsdghrewt4bfdvd\r\ngs gdl;a";
            var actual = new CatCommand().Execute("../../../TestFiles/file.txt");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WcGoodArgsTest()
        {
            var expected = "44 bytes\n4 lines\n6 words";
            var actual = new WcCommand().Execute("../../../TestFiles/file.txt");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PwdTest()
        {
            var previousDirectory = Directory.GetCurrentDirectory();
            new CdCommand().Execute("../../../TestFiles");
            var expected = Directory.GetCurrentDirectory() + "\n" + Directory.GetCurrentDirectory() + "\\file.txt";
            var actual = new PwdCommand().Execute("file.txt");
            new CdCommand().Execute(previousDirectory);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CdGoodArgsTest()
        {
            var previousDirectory = Directory.GetCurrentDirectory();
            new CdCommand().Execute("..");
            var newDirectory = Directory.GetCurrentDirectory();
            new CdCommand().Execute(previousDirectory);
            Assert.AreEqual(Directory.GetParent(previousDirectory).ToString(), newDirectory);
        }

        [Test]
        public void EchoTest()
        {
            var expected = "123 abc \n 321321";
            var actual = new EchoCommand().Execute("123 abc \n 321321");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CatBadArgsTest()
        {
            try
            {
                new CatCommand().Execute("file");
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void WcBadArgsTest()
        {
            try
            {
                new WcCommand().Execute("../../../TestFiles/file.txt ../../../TestFiles/file.txt");
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void CdBadArgsTest()
        {
            try
            {
                new CdCommand().Execute("../../../TestFiles/file.txt");
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }
    }
}