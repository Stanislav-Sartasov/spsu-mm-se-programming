using Bash.Commands;
using NUnit.Framework;

namespace Bash.UnitTests.CommandsTests
{
    internal class ListFilesTest
    {
        ListFiles ls = new ListFiles();

        [Test]
        public void FindFilesTest()
        {
            TestDirectory.SetDirectory();
            var result = ls.Execute(new string[0]);
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("TestFile.txt"));
            Assert.That(result[1], Is.EqualTo("TestFileEmpty.txt"));
            Assert.That(result[2], Is.EqualTo("TestFolder"));
        }

        [Test]
        public void FindNoFiles()
        {
            TestDirectory.SetFolder();
            var a = Environment.CurrentDirectory;
            var result = ls.Execute(new string[0]);
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("No files in this directory."));
        }
    }
}
