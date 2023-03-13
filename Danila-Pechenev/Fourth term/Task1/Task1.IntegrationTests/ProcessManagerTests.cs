using NUnit.Framework;

using ProcessManager;

namespace Task1.IntegrationTests;

public class ProcessManagerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, Timeout(60000)]
    public void TwoProcessesPriorityPolicyTest()
    {
        var processes = new List<Process>();
        for (int i = 0; i < 2; i++)
        {
            processes.Add(new Process());
        }

        ProcessManager.ProcessManager.Run(processes, withPriority: true);

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void TwoProcessesNonPriorityPolicyTest()
    {
        var processes = new List<Process>();
        for (int i = 0; i < 2; i++)
        {
            processes.Add(new Process());
        }

        ProcessManager.ProcessManager.Run(processes, withPriority: false);

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void FiveProcessesPriorityPolicyTest()
    {
        var processes = new List<Process>();
        for (int i = 0; i < 5; i++)
        {
            processes.Add(new Process());
        }

        ProcessManager.ProcessManager.Run(processes, withPriority: true);

        Assert.Pass();
    }

    [Test, Timeout(60000)]
    public void FiveProcessesNonPriorityPolicyTest()
    {
        var processes = new List<Process>();
        for (int i = 0; i < 5; i++)
        {
            processes.Add(new Process());
        }

        ProcessManager.ProcessManager.Run(processes, withPriority: false);

        Assert.Pass();
    }
}
