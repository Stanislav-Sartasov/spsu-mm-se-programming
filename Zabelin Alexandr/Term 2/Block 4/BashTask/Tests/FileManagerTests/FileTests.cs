using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManager;

namespace FileManagerTests
{
    [TestClass]
    public class FileTests
    {
        private string pathToSmallFile = @"../../../SmallTestFile.txt";
        private string pathToLargeFile = @"../../../LargeTestFile.txt";

        [TestMethod]
        public void WordsCountTestSmallFile()
        {
            File file = new File(pathToSmallFile);

            long expected = 116;
            long actual = file.WordsCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordsCountTestLargeFile()
        {
            File file = new File(pathToLargeFile);

            long expected = 2495;
            long actual = file.WordsCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LinesCountTestSmallFile()
        {
            File file = new File(pathToSmallFile);

            long expected = 4;
            long actual = file.LinesCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LinesCountTestLargeFile()
        {
            File file = new File(pathToLargeFile);

            long expected = 39;
            long actual = file.LinesCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeightTestSmallFile()
        {
            File file = new File(pathToSmallFile);

            long expected = 758;
            long actual = file.Weight;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeightTestLargeFile()
        {
            File file = new File(pathToLargeFile);

            long expected = 16829;
            long actual = file.Weight;

            Assert.AreEqual(expected, actual);
        }
    }
}