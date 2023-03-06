namespace Fibers.Tests;

public class Tests
{
    [Test, Timeout(60000)]
    public void RoundRobin1Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.RoundRobin))
        {
            for (var i = 0; i < 1; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void Prioritized1Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.Prioritized))
        {
            for (var i = 0; i < 1; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }


    [Test, Timeout(60000)]
    public void RoundRobin2Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.RoundRobin))
        {
            for (var i = 0; i < 2; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void Prioritized2Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.Prioritized))
        {
            for (var i = 0; i < 2; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }


    [Test, Timeout(60000)]
    public void RoundRobin5Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.RoundRobin))
        {
            for (var i = 0; i < 5; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void Prioritized5Test()
    {
        using (var pm = ProcessManagerFactory.Create(ProcessManagerStrategy.Prioritized))
        {
            for (var i = 0; i < 5; i++) pm.AddTask(new Process(pm));
            pm.Run();
        }

        Assert.Pass();
    }
}