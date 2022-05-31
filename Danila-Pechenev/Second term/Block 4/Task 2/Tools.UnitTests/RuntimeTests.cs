namespace Tools.UnitTests;
using NUnit.Framework;
using Tools;

public class RuntimeTests
{
    [Test]
    public void AddNonexistentVariableTest()
    {
        Runtime.AddVariable("name", "Danya");
        Assert.IsTrue(Runtime.LocalVariables.ContainsKey("name"));
        Assert.AreEqual("Danya", Runtime.LocalVariables["name"]);
    }

    [Test]
    public void AddExistentVariableTest()
    {
        Runtime.AddVariable("var", "5");
        Runtime.AddVariable("var", "7");
        Assert.IsTrue(Runtime.LocalVariables.ContainsKey("var"));
        Assert.AreEqual("7", Runtime.LocalVariables["var"]);
    }

    [Test]
    public void GetVariableTest()
    {
        Runtime.AddVariable("name", "Tom");
        Assert.AreEqual("Tom", Runtime.GetVariable("name"));
    }

    [Test]
    public void GetGeneralVariableTest()
    {
        Assert.IsTrue(Runtime.GetVariable("PATH")?.Length > 10);
    }

    [Test]
    public void GetNonexistentVariableTest()
    {
        Assert.AreEqual(null, Runtime.GetVariable("vvvvv"));
    }
}
