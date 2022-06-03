using NUnit.Framework;
using System.Reflection;

namespace Bash.UnitTests.ToolsTests
{
    internal class BashCoreTests
    {
        BashCore core = new BashCore();
        MethodInfo? method = typeof(BashCore).GetMethod("ProcessCommands", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo? field = typeof(BashCore).GetField("locals", BindingFlags.Instance | BindingFlags.NonPublic);

        VariableStorage storage
        {
            get => (VariableStorage)field?.GetValue(core);
        }

        [Test]
        public void ProcessBadInputTest()
        {
            var result = InvokeWith(new object?[] { null });
            Assert.That(result, Is.EqualTo(new string[] { "Incorrect input, failed to recognize commands" }));
            result = InvokeWith(new object?[] { "block block block \"" });
            Assert.That(result, Is.EqualTo(new string[] { "Incorrect input, failed to recognize commands" }));
        }

        [Test]
        public void ProcessWellAddVariableTest()
        {
            core = new BashCore();
            var result = InvokeWith(new object[] { "$a=5 $b=10 $c=15" });
            Assert.That(result.Length, Is.EqualTo(0));
            Assert.That(storage.Get("a"), Is.EqualTo("5"));
            Assert.That(storage.Get("b"), Is.EqualTo("10"));
            Assert.That(storage.Get("c"), Is.EqualTo("15"));
            Assert.That(storage.Get("haha"), Is.EqualTo(""));
        }

        [Test]
        public void ProcessBadlyAddVariableTest()
        {
            core = new BashCore();
            string input = "$a =5          $b= 10             $c = 15           $ d       e=5";
            var result = InvokeWith(new object[] { input });
            Assert.That(result.Length, Is.EqualTo(10));
            string[] actual = input.Split(" ").Where(x => x != "" && x != " ").ToArray();

            for (int i = 0; i < result.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo($"Command {actual[i]} not found."));
            }
        }

        [Test]
        public void ProcessPipesTest()
        {
            core = new BashCore();
            var result = InvokeWith(new object[] { "cd ..\\..\\..\\..\\Bash.UnitTests\\TestFiles  |   ls  |  cat" });
            Assert.That(result.Length, Is.EqualTo(4));
            int i = 0;
            Assert.That(result[i++], Is.EqualTo("Filename: TestFile.txt"));
            Assert.That(result[i++], Is.EqualTo("In this world, nothing is perfect as long as it is the work of people."));
            Assert.That(result[i++], Is.EqualTo("Filename: TestFileEmpty.txt"));
            Assert.That(result[i++], Is.EqualTo("Filename: TestFolder does not exist..."));
        }

        [Test]
        public void ProcessBadlyApp()
        {
            var result = InvokeWith(new string[] { "thisnoapp&7?#!" });
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("Application not found"));
        }

        private string[] InvokeWith(object?[]? parameters)
        {
            var result = method?.Invoke(core, parameters);
            Assert.IsNotNull(result);
            return (string[])result;
        }

    }
}
