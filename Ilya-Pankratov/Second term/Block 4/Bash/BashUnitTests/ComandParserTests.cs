using Bash;
using NUnit.Framework;

namespace BashUnitTests
{
    public class ComandParserTests
    {
        [Test]
        public void ParseTest()
        {
            var input = "    cat \"$myfile\" | echo  \"file size is $filesize\"   ";
            var parserResult = CommandParser.Parse(input);
            var expectedResult = new string[]
            {
                "cat", "$myfile", "|", "echo", "file size is $filesize"
            };

            Assert.IsTrue(parserResult.Length == expectedResult.Length);

            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], parserResult[i]);
            }
        }
    }
}