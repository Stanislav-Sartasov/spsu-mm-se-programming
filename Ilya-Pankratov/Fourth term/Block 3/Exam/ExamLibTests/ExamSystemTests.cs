using ExamLib;
using ExamLib.HashTables;

namespace ExamLibTests;

[TestFixture(typeof(ExamSystem<CoreCockooHashSet<StudentPassedExam>>))]
[TestFixture(typeof(ExamSystem<StripedCuckooHashSet<StudentPassedExam>>))]
[TestFixture(typeof(ExamSystem<StripedHashSet<StudentPassedExam>>))]
public class ExamSystemTests<TExamSystem> where TExamSystem : IExamSystem, new()
{
    private IExamSystem examSystem;

    readonly List<Tuple<int, int>> testData = new()
    {
        new Tuple<int, int>(1, 2), new Tuple<int, int>(3, 4),
        new Tuple<int, int>(5, 6), new Tuple<int, int>(7, 8),
        new Tuple<int, int>(9, 10), new Tuple<int, int>(11, 12),
        new Tuple<int, int>(13, 14), new Tuple<int, int>(15, 16),
        new Tuple<int, int>(17, 18), new Tuple<int, int>(19, 20)
    };

    [SetUp]
    public void Setup()
    {
        examSystem = new TExamSystem();
    }

    private void ThreadAdd(Tuple<int, int> value)
    {
        examSystem.Add(value.Item1, value.Item2);
    }

    private void ThreadRemove(Tuple<int, int> value)
    {
        examSystem.Remove(value.Item1, value.Item2);
    }

    private bool ThreadContains(Tuple<int, int> value)
    {
        return examSystem.Contains(value.Item1, value.Item2);
    }

    private void LaunchThreadActivity(Action<Tuple<int, int>> function)
    {
        var threads = testData.Select(data => new Thread(() => function(data))).ToList();

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    [Test]
    public void OneThreadAddTest()
    {
        ThreadAdd(testData.First());
        Assert.That(examSystem.Count, Is.EqualTo(1));
    }

    [Test]
    public void OneThreadRemoveTest()
    {
        ThreadAdd(testData.First());
        Assert.That(examSystem.Count, Is.EqualTo(1));
        ThreadRemove(testData.First());
        Assert.That(examSystem.Count, Is.Zero);
    }

    [Test]
    public void OneThreadContainsTest()
    {
        ThreadAdd(testData.First());
        Assert.That(ThreadContains(testData.First()), Is.True);
        Assert.That(examSystem.Count, Is.EqualTo(1));
    }

    [Test]
    public void TenThreadAddTest()
    {
        LaunchThreadActivity(ThreadAdd);

        foreach (var data in testData)
        {
            Assert.That(ThreadContains(data), Is.True);
        }

        Assert.That(examSystem.Count, Is.EqualTo(testData.Count));
    }

    [Test]
    public void TenThreadRemoveTest()
    {
        LaunchThreadActivity(ThreadAdd);
        foreach (var data in testData)
        {
            Assert.That(ThreadContains(data));
        }

        LaunchThreadActivity(ThreadRemove);
        Assert.That(examSystem.Count, Is.Zero);
    }

    [Test]
    public void ResizeTest()
    {
        var number = 10000;
        var random = new Random();
        var minCourseId = 1;
        var maxCourseId = 20;
        for (var i = 0; i < number; i++)
        {
            examSystem.Add(i, random.Next(minCourseId, maxCourseId));
        }

        Assert.That(examSystem.Count, Is.EqualTo(number));
    }
}