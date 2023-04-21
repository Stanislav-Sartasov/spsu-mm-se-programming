using BashComponents;
using NUnit.Framework;

namespace BashComponentsUnitTests;

public class InputParserTests
{
    [Test]
    public void ParseTest()
    {
        var parser = new InputParser();
        var inputValue = "       firstLexeme     secondLexeme \"some   very long lexeme and $someVar\"        ";
        var correctResult = new List<String> { "firstLexeme", "secondLexeme", "some   very long lexeme and $someVar" };
        var parseResult = parser.Parse(inputValue);
        Assert.That(parseResult.Count, Is.EqualTo(3));
        foreach (var entity in correctResult)
        {
            Assert.That(parseResult.Contains(entity));
        }
    }
}