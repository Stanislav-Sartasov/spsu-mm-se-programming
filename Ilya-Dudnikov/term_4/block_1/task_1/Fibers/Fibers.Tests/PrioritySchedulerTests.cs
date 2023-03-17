using Fibers.Fibers;
using Fibers.ProcessManager;
using static Fibers.ProcessManager.ProcessManager;

namespace Fibers.Tests;

public class PrioritySchedulerTests
{
    private FiberScheduler scheduler;

    [SetUp]
    public void Setup()
    {
        scheduler = new PriorityScheduler();
    }

    [Test]
    public void ScheduleProcessTestOneProcess()
    {
        Run(new List<Process> { new() }, scheduler);
    }

    [Test]
    public void ScheduleProcessTestFiveProcesses()
    {
        var processList = Enumerable.Range(0, 5).Select(_ => new Process()).ToList();
        Run(processList, scheduler);
    }

    [Test]
    public void ScheduleProcessTestTenProcesses()
    {
        var processList = Enumerable.Range(0, 10).Select(_ => new Process()).ToList();
        Run(processList, scheduler);
    }

    [Test]
    public void RemoveProcessOnEmpty()
    {
        Assert.Throws<InvalidOperationException>(() => scheduler.RemoveRunningFiber());
    }

    [Test]
    public void ExecuteOnEmpty()
    {
        Assert.Throws<InvalidOperationException>(() => scheduler.Execute());
    }

    [Test]
    public void RunNextFiberOnEmpty()
    {
        Assert.Throws<InvalidOperationException>(() => scheduler.RunNextFiber());
    }
}