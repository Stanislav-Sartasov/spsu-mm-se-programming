using NUnit.Framework;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;


namespace Task8.Tests
{
    public class CommandTests
    {

        [Test]
        public void EchoTest()
        {
            string expected = "test";

            var res = new Interpreter("TestScripts/echo_simple.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void EchoWithVariableTest()
        {
            string expected = "it is variable value";

            var res = new Interpreter("TestScripts/echo_with_variable.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void EchoWithVariablesTest()
        {
            string expected = "2 * 5 = 10";

            var res = new Interpreter("TestScripts/echo_with_variables.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void ExitTest()
        {
            string expected = "exit text";

            var res = new Interpreter("TestScripts/exit.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void PwdTest()
        {
            StringBuilder resultBuilder = new StringBuilder();

            resultBuilder.AppendLine(Environment.CurrentDirectory);
            var fileNames = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly)
                                     .Select(path => Path.GetFileName(path)).ToList();

            fileNames.ForEach(file => resultBuilder.AppendLine(file));


            var res = new Interpreter("TestScripts/pwd_simple.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(resultBuilder.ToString()?.TrimEnd('\r', '\n')));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        

        [Test]
        public void CatTest()
        {
            string expected = File.ReadAllText(Path.GetFullPath("TestScripts/echo_with_variables.txt"));

            var res = new Interpreter("TestScripts/cat.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void CatWithDoesnotExistsFileTest()
        {
            var filename = Path.GetFullPath("test.txt");

            var res = new Interpreter("TestScripts/cat_doesnot_exists_file.txt")
                .ExecuteScript(out string result, out string error);

            Assert.False(res);
            Assert.That(result, Is.EqualTo(string.Empty));
            Assert.That(error, Is.EqualTo($"File {filename} not found"));

            Assert.Pass();
        }

        [Test]
        public void WcTest()
        {
            string fileText = File.ReadAllText(Path.GetFullPath("TestScripts/echo_with_variable.txt"));

            var bytesCount = File.ReadAllBytes(Path.GetFullPath("TestScripts/echo_with_variable.txt")).Length;

            var linesCount = fileText.Split(Environment.NewLine).Length;

            var wordsCount = Regex.Matches(fileText, @"\w+").Count;

            var extected = $"Lines count: {linesCount}{Environment.NewLine}" +
                           $"Words count: {bytesCount}{Environment.NewLine}" +
                           $"Bytes count: {wordsCount}";

            var res = new Interpreter("TestScripts/wc.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(extected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }


        [Test]
        public void StartNotepadProcessTest()
        {
            var processes = Process.GetProcessesByName("notepad");
            if (processes.Length > 0)
            {
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }

            var res = new Interpreter("TestScripts/run_notepad.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);

            processes = Process.GetProcessesByName("notepad");

            Assert.That(processes.Length, Is.EqualTo(1));

            Assert.That(result, Is.EqualTo(string.Empty));
            Assert.That(error, Is.EqualTo(string.Empty));

            processes[0].Kill();

            Assert.Pass();
        }


        [Test]
        public void PipeCommandTest()
        {
            string expected = File.ReadAllText(Path.GetFullPath("TestScripts/cat.txt"));

            var res = new Interpreter("TestScripts/pipe.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void InvalidVariableFormatTest()
        {
            try
            {
                var res = new Interpreter("TestScripts/format_exception.txt")
                    .ExecuteScript(out string result, out string error);
                Assert.Fail();
            }
            catch (FormatException)
            {
                Assert.Pass();
            }
        }


        [Test]
        public void EchoConsoleTest()
        {
            string expected = "It's the correct result";

            var res = new Interpreter("echo It's the correct result")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void EchoWithVariablesConsoleTest()
        {
            string expected = "My name is Tom";
            var variable = new Interpreter("$name=Tom");
            var res = new Interpreter("echo My name is $name")
                .ExecuteScript(out string result, out string error);
            

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void EchoWithVariablesExceptionConsoleTest()
        {
            var ex = Assert.Throws<FormatException>(() => new Interpreter("$var 10"));

            Assert.That(ex.Message, Is.EqualTo("Variable assignment symbol ('=') not found"));

            Assert.Pass();
        }

        [Test]
        public void CatConsoleTest()
        {
            string expected = File.ReadAllText(Path.GetFullPath("TestScripts/echo_with_variables.txt"));

            var res = new Interpreter("cat TestScripts/echo_with_variables.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void CatWithDoesnotExistsFileConsoleTest()
        {
            var filename = Path.GetFullPath("test.txt");

            var res = new Interpreter("cat test.txt")
                .ExecuteScript(out string result, out string error);

            Assert.False(res);
            Assert.That(result, Is.EqualTo(string.Empty));
            Assert.That(error, Is.EqualTo($"File {filename} not found"));

            Assert.Pass();
        }

        [Test]
        public void WcConsoleTest()
        {
            string fileText = File.ReadAllText(Path.GetFullPath("TestScripts/echo_with_variable.txt"));

            var bytesCount = File.ReadAllBytes(Path.GetFullPath("TestScripts/echo_with_variable.txt")).Length;

            var linesCount = fileText.Split(Environment.NewLine).Length;

            var wordsCount = Regex.Matches(fileText, @"\w+").Count;

            var extected = $"Lines count: {linesCount}{Environment.NewLine}" +
                           $"Words count: {bytesCount}{Environment.NewLine}" +
                           $"Bytes count: {wordsCount}";

            var res = new Interpreter("wc TestScripts/echo_with_variable.txt")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(extected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }

        [Test]
        public void PipeCommandConsoleTest()
        {
            string expected = File.ReadAllText(Path.GetFullPath("TestScripts/cat.txt"));

            var res = new Interpreter("echo TestScripts/cat.txt | cat")
                .ExecuteScript(out string result, out string error);

            Assert.True(res);
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(error, Is.EqualTo(string.Empty));

            Assert.Pass();
        }
    }
}