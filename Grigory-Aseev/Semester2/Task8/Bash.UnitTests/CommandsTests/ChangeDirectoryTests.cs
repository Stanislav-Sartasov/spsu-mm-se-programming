using Bash.Commands;
using NUnit.Framework;

namespace Bash.UnitTests.CommandsTests
{
    internal class ChangeDirectoryTests
    {
        ChangeDirectory cd = new ChangeDirectory();

        [Test]
        public void SetUpperDirectoryTest()
        {
            string path = Environment.CurrentDirectory;
            string? newPath = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
            Assert.IsNull(cd.Execute(new string[] { ".." }));
            Assert.That(Environment.CurrentDirectory, Is.EqualTo(newPath));
        }

        [Test]
        public void SetNotADirectoryTest()
        {
            string[]? result = cd.Execute(new string[] { string.Empty });
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Failed to put the directory, the reasons may be: too many arguments or the wrong path"));
        }

        [Test]
        public void SetHomeDirectory()
        {
            AssertSetCorrectnessHomeDir(new string[0]);
            AssertSetCorrectnessHomeDir(new string[] { "~" });
        }

        [Test]
        public void SetUserDirectory()
        {
            List<string[]> users = Directory.GetDirectories(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) ?? "").Select(x => new string[] { GetNameFolder(x), x }).ToList();
            foreach (var user in users)
            {
                try
                {
                    Directory.SetCurrentDirectory(user[1]);
                }
                catch (Exception)
                {
                    continue;
                }

                Assert.IsNull(cd.Execute(new string[] { $"~{user[0]}" }));
                Assert.That(GetNameFolder(Environment.CurrentDirectory), Is.EqualTo($"{user[0]}"));
            }
        }

        private void AssertSetCorrectnessHomeDir(string[] command)
        {
            Assert.IsNull(cd.Execute(new string[] { ".." }));
            Assert.IsNull(cd.Execute(command));
            Assert.That(Environment.CurrentDirectory, Is.EqualTo(cd.HomePath));
        }

        private string GetNameFolder(string path) => path.Substring(path.LastIndexOf('\\') + 1);
    }
}
