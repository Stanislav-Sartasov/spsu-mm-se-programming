using NUnit.Framework;

namespace Bash.UnitTests.ToolsTests
{
    internal class ParserTests
    {
        [Test]
        public void ParseBadCommandTest()
        {
            Assert.IsNull(Parser.GetCommands("lala lala \"blok\"  \"unclosed block"));
            Assert.IsNull(Parser.GetCommands(null));
        }

        [Test]
        public void ParseCommandTest()
        {
            List<string[]>? result = Parser.GetCommands("         shadow    raze.txt                 \"shadow   raze.txt\"        \"\"        ");
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Length, Is.EqualTo(4));
            string[] expected = new string[] { "shadow", "raze.txt", "shadow   raze.txt", "" };
            for (int i = 0; i < result[0].Length; i++)
            {
                Assert.That(expected[i], Is.EqualTo(result[0][i]));
            }
        }

        [Test]
        public void ParseCommandsTest()
        {
            List<string[]>? result = Parser.GetCommands("    Strengthisnotaresult.    ||||      Strengthistheprocessofpersistentmovementtowardsagoal.   |          |              |");
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Length, Is.EqualTo(1));
            Assert.That(result[1].Length, Is.EqualTo(1));
            string[] expected = new string[] { "Strengthisnotaresult.", "Strengthistheprocessofpersistentmovementtowardsagoal." };
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(expected[i], Is.EqualTo(result[i][0]));
            }
        }
    }
}
