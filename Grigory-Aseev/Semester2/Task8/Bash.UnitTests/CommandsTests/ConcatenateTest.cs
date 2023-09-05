using Bash.Commands;
using NUnit.Framework;

namespace Bash.UnitTests.CommandsTests
{
    internal class ConcatenateTest
    {
        Concatenate cat = new Concatenate();

        [Test]
        public void ConcatenateWellTest()
        {

            string file = "TestFile.txt";
            var result = ConcatenateFile(file);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo($"Filename: {file}"));
            Assert.That(result[1], Is.EqualTo("In this world, nothing is perfect as long as it is the work of people."));
        }

        [Test]
        public void ConcatenateBadlyTest()
        {
            string file = "There's nothing here";
            var result = ConcatenateFile(file);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo($"Filename: {file} does not exist..."));
        }

        private string[] ConcatenateFile(string file)
        {
            TestDirectory.SetDirectory();
            var result = cat.Execute(new string[] { file });
            Assert.IsNotNull(result);
            return result;
        }
    }
}
