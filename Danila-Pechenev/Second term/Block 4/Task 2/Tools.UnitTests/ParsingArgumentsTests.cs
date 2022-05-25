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
        var result = Interpreter.ParseArguments("", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("", string.Join(' ', result));
        }
    }

    [Test]
    public void OneWordWithoutQuotesTest()
    {
        var result = Interpreter.ParseArguments("town", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("town", string.Join(' ', result));
        }
    }

    [Test]
    public void TwoWordsWithoutQuotesTest()
    {
        var result = Interpreter.ParseArguments("apple orange", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("apple orange", string.Join(' ', result));
        }
    }

    [Test]
    public void TwoWordsWithoutQuotesWithÑommaTest()
    {
        var result = Interpreter.ParseArguments("word1, word2", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("word1, word2", string.Join(' ', result));
        }
    }

    [Test]
    public void SentenceInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"Hello, world!\"", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Hello, world!", string.Join(' ', result));
        }
    }

    [Test]
    public void SentenceInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'abc 123'", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("abc 123", string.Join(' ', result));
        }
    }

    [Test]
    public void LineWithVariableTest()
    {
        interpreter.ExecuteLine("s=\"math\"", out string line);
        var result = Interpreter.ParseArguments("I like $s", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("I like math", string.Join(' ', result));
        }
    }

    [Test]
    public void LineWithUnknownVariableTest()
    {
        interpreter.ExecuteLine("abc=123", out string line);
        var result = Interpreter.ParseArguments("\"this is\" $cba", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("this is", string.Join(' ', result));
        }
    }

    [Test]
    public void LineWithSystemVariableTest()
    {
        var result = Interpreter.ParseArguments("echo $PATH", true);
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
        var result = Interpreter.ParseArguments("\"Hi, $name!\"", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Hi, Danya!", string.Join(' ', result));
        }
    }

    [Test]
    public void SingleQuotesWithVariableTest()
    {
        interpreter.ExecuteLine("name=Danya", out string line);
        var result = Interpreter.ParseArguments("'Hi, $name!'", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Hi, $name!", string.Join(' ', result));
        }
    }

    [Test]
    public void BackslashInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"\\\\\"", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("\\", string.Join(' ', result));
        }
    }

    [Test]
    public void BackslashInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'\\\\'", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("\\\\", string.Join(' ', result));
        }
    }

    [Test]
    public void DifferentPatternsTest()
    {
        interpreter.ExecuteLine("who=you", out string line);
        var result = Interpreter.ParseArguments("\"Hello\", how a're' $who?", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Hello, how are you?", string.Join(' ', result));
        }
    }

    [Test]
    public void DoubleQuotesInSingleQuotesTest()
    {
        var result = Interpreter.ParseArguments("'His name is \"Andrei\"'", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("His name is \"Andrei\"", string.Join(' ', result));
        }
    }

    [Test]
    public void SingleQuotesInDoubleQuotesTest()
    {
        var result = Interpreter.ParseArguments("\"It's 'a'!\"", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("It's 'a'!", string.Join(' ', result));
        }
    }

    [Test]
    public void ShieldingTest()
    {
        var result = Interpreter.ParseArguments("Working\\ directiry/folder/file.txt", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Working directiry/folder/file.txt", string.Join(' ', result));
        }
    }

    [Test]
    public void ArgumentWithEqualSignTest()
    {
        var result = Interpreter.ParseArguments("v=123", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("v=123", string.Join(' ', result));
        }
    }

    [Test]
    public void SkipUnknownCharacterTest()
    {
        var result = Interpreter.ParseArguments("Hi, $var!", true);
        if (result == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.AreEqual("Hi, !", string.Join(' ', result));
        }
    }

    [Test]
    public void NullArgumentsTest()
    {
        var result = Interpreter.ParseArguments("abc'", true);
        Assert.AreEqual(null, result);
    }
}
