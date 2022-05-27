using Bash;
using NUnit.Framework;

namespace BashTests
{
    public class BashTests
    {
        BashInterpreter bash = new BashInterpreter();

        private string[] InvokeParse(string? args)
        {
            var result = (string[])typeof(BashInterpreter)
                .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .Invoke(bash, new string[] { args });
            return result;
        }

        [Test]
        public void GoodCommandsParseTest()
        {
            var parsed = InvokeParse("cat 5 10");
            Assert.AreEqual("cat", parsed[0]);
            Assert.AreEqual("5 10 ", parsed[1]);
            parsed = InvokeParse("$var=123");
            Assert.AreEqual("", parsed[0]);
            Assert.AreEqual("", parsed[1]);
        }

        [Test]
        public void BadCommandsParseTest()
        {
            try
            {
                InvokeParse("");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Empty request", ex.InnerException.Message);
            }

            try
            {
                InvokeParse("$var 123");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error creating a local variable, variable assignment pattern: $var=value", ex.InnerException.Message);
            }

            try
            {
                InvokeParse("^cat file.txt");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid name, the command must start with a letter", ex.InnerException.Message);
            }

            try
            {
                InvokeParse("echo \"qwerty");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Missing closing quotation mark", ex.InnerException.Message);
            }
        }

        [Test]
        public void PipelineTest()
        {
            var command = "$var=../../../TestFiles/file.txt";
            var result = (string?)typeof(BashInterpreter)
                .GetMethod("ExecuteCommands", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .Invoke(bash, new string[] { command });
            Assert.AreEqual("", result);

            command = "echo $var | cat";
            result = (string?)typeof(BashInterpreter)
               .GetMethod("ExecuteCommands", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
               .Invoke(bash, new string[] { command });
            Assert.AreEqual("231 5opsaf\r\nfsdhjf\r\nsdghrewt4bfdvd\r\ngs gdl;a", result);
        }
    }
}