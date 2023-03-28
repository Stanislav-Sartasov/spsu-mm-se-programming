using System.Text.RegularExpressions;
using ProducerConsumer;

namespace ProducerConsumerTests;

public partial class FactoryTests
{
    [GeneratedRegex("[(]\\d+[)]: consumer change applications' number from \\d+ to \\d+(\r\n|\r|\n)", RegexOptions.Compiled)]
    private static partial Regex ConsumerRegex();
    
    [Test]
    public void StartTest()
    {
        var producers = 3;
        var consumers = 6;
        var timeout = 1000;
        var factory = new Factory(producers, consumers);
        Factory.UpdateTimeout(timeout);
        Factory.DisableConsoleLogs();
        factory.Start();
        Thread.Sleep(3000);
        factory.Stop();
        factory.Dispose();
        Assert.Pass();
    }

    [Test]
    public void DoubleStartTest()
    {
        var producers = 5;
        var consumers = 5;
        var timeout = 1000;
        var factory = new Factory(producers, consumers);
        Factory.UpdateTimeout(timeout);
        Factory.DisableConsoleLogs();
        factory.Start();
        Assert.Catch<InvalidOperationException>(factory.Start);
    }

    [Test]
    public void ConsoleLoggingTest()
    {
        var producers = 0;
        var consumers = 2;
        var timeout = 3000;
        var factory = new Factory(producers, consumers);
        Factory.UpdateTimeout(timeout);
        Factory.EnableConsoleLogs();

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        factory.Start();
        factory.Stop();
        factory.Dispose();
    
        var consoleOutput = stringWriter.ToString();
        stringWriter.Dispose();
        
        var regex = ConsumerRegex();
        Assert.That(regex.Matches(consoleOutput), Is.Not.Empty);
    }
}
