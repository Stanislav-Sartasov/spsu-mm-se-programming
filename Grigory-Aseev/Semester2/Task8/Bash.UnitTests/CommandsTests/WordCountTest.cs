using Bash.Commands;
using NUnit.Framework;

namespace Bash.UnitTests.CommandsTests
{
    internal class WordCountTest
    {
        WordCount wc = new WordCount();

        [Test]
        public void CountWellTest()
        {
            TestDirectory.SetDirectory();
            string file = "TestFile.txt";
            var result = ExecuteCommand(file);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo($"Filename: {file}"));
            Assert.That(result[1], Is.EqualTo("Lines: 1\tWords: 15\tBytes: 73"));
        }

        [Test]
        public void CountEmptyTest()
        {
            TestDirectory.SetDirectory();
            string file = "TestFileEmpty.txt";
            var result = ExecuteCommand(file);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo($"Filename: {file}"));
            Assert.That(result[1], Is.EqualTo("Lines: 0\tWords: 0\tBytes: 0"));

        }

        [Test]
        public void CountBadlyTest()
        {
            TestDirectory.SetFolder();
            string file = "There's nothing here";
            var result = ExecuteCommand(file);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo($"Filename: {file} does not exist..."));
        }

        private string[] ExecuteCommand(string file)
        {
            var result = wc.Execute(new string[] { file });
            Assert.IsNotNull(result);
            return result;
        }
    }
}
