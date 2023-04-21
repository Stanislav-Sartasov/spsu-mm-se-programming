using BashComponents;
using NUnit.Framework;

namespace BashComponentsUnitTests;

public class VariableContainerTests
{
    [Test]
    public void AddAndGetTest()
    {
        var container = new VariableContainer();
        var name = "myVar";
        var value = "myValue";
        container.Add(name, value);
        Assert.That(value, Is.EqualTo(container.Get(name)));
    }
}