using BashComponents;
using NUnit.Framework;

namespace BashComponentsUnitTests;

public class LexemeConverterTests
{
    [Test]
    public void AddAndUnpackVariableTest()
    {
        var converter = new LexemeConverter();
        var lexemes = new List<String> { "cat", "fileName.txt", "|", "echo", "someText" };
        var convertResult = converter.Run(lexemes);
        Assert.That(convertResult.Count(), Is.EqualTo(2));
        Assert.That(convertResult.First().Name, Is.EqualTo("cat"));
        Assert.That(convertResult.First().Arguments.Count, Is.EqualTo(1));
        Assert.That(convertResult[1].Name, Is.EqualTo("echo"));
        Assert.That(convertResult[1].Arguments.Count, Is.EqualTo(1));
    }
}