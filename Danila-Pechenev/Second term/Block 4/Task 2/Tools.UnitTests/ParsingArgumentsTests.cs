namespace Tools.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;

public class ParsingArgumentsTests
{
    static List<ICommand> listOfCommands = new List<ICommand>
        {
            new Echo(),
            new Pwd(),
            new Ls(),
            new Cd(),
            new Cat(),
            new Wc()
        };

    Interpreter interpreter = new Interpreter(listOfCommands);

    [Test]
    public void EmptyArgumentsLineTest()
    {
        var result = Interpreter.ParseArguments("");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "");
        }
    }

    [Test]
    public void OneWordWithoutQuotesTest()
    {
        var result = Interpreter.ParseArguments("town");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "town");
        }
    }

    [Test]
    public void TwoWordsWithoutQuotesTest()
    {
        var result = Interpreter.ParseArguments("apple orange");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "apple orange");
        }
    }

    [Test]
    public void TwoWordsWithoutQuotesWithÑommaTest()
    {
        var result = Interpreter.ParseArguments("word1, word2");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "word1, word2");
        }
    }

    [Test]
    public void SentenceInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"Hello, world!\"");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Hello, world!");
        }
    }

    [Test]
    public void SentenceInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'abc 123'");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "abc 123");
        }
    }

    [Test]
    public void LineWithVariableTest()
    {
        interpreter.ExecuteLine("s=\"math\"", out string line);
        var result = Interpreter.ParseArguments("I like $s");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "I like math");
        }
    }

    [Test]
    public void LineWithUnknownVariableTest()
    {
        interpreter.ExecuteLine("abc=123", out string line);
        var result = Interpreter.ParseArguments("\"this is\" $cba");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "this is");
        }
    }

    [Test]
    public void LineWithSystemVariableTest()
    {
        var result = Interpreter.ParseArguments("echo $PATH");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.IsTrue(string.Join(' ', result).Length > 10);
        }
    }

    [Test]
    public void DoubleQuotesWithVariableTest()
    {
        interpreter.ExecuteLine("name=Danya", out string line);
        var result = Interpreter.ParseArguments("\"Hi, $name!\"");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Hi, Danya!");
        }
    }

    [Test]
    public void SingleQuotesWithVariableTest()
    {
        interpreter.ExecuteLine("name=Danya", out string line);
        var result = Interpreter.ParseArguments("'Hi, $name!'");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Hi, $name!");
        }
    }

    [Test]
    public void BackslashInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"\\\\\"");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "\\");
        }
    }

    [Test]
    public void BackslashInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'\\\\'");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "\\\\");
        }
    }

    [Test]
    public void DifferentPatternsTest()
    {
        interpreter.ExecuteLine("who=you", out string line);
        var result = Interpreter.ParseArguments("\"Hello\", how a're' $who?");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Hello, how are you?");
        }
    }

    [Test]
    public void DoubleQuotesInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'His name is \"Andrei\"'");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "His name is \"Andrei\"");
        }
    }

    [Test]
    public void SingleQuotesInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"It's 'a'!\"");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "It's 'a'!");
        }
    }

    [Test]
    public void ShieldingTest()
    {
        var result = Interpreter.ParseArguments("Working\\ directiry/folder/file.txt");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Working directiry/folder/file.txt");
        }
    }

    [Test]
    public void ArgumentWithEqualSignTest()
    {
        var result = Interpreter.ParseArguments("v=123");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "v=123");
        }
    }

    [Test]
    public void SkipUnknownCharacterTest()
    {
        var result = Interpreter.ParseArguments("Hi, $var!");
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual(string.Join(' ', result), "Hi, !");
        }
    }

    [Test]
    public void NullArgumentsTest()
    {
        var result = Interpreter.ParseArguments("abc'");
        Assert.AreEqual(null, result);
    }
}
