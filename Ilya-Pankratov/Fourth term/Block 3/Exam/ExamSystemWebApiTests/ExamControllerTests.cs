using ExamLib;
using ExamSystemWebApi;
using ExamSystemWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;

namespace ExamSystemWebApiTests;

public class ExamControllerTests
{
    private ExamController controller;

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
        var examSystem = new ExamSystem<StripedHashSet<StudentPassedExam>>();
        controller = new ExamController(examSystem);
    }

    [Test]
    public void AddTest()
    {
        var pair = testData.First();

        var actionResult = controller.Add(pair.Item1, pair.Item2);

        var okResult = actionResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.EqualTo(null));
        var value = okResult.Value as StudentStatusResult;
        Assert.That(value != null);
        Assert.That(value.StudentId, Is.EqualTo(pair.Item1));
        Assert.That(value.CourseId, Is.EqualTo(pair.Item2));
        Assert.That(value.StatusResult, Is.EqualTo(StudentStatusResult.Added));
    }

    [Test]
    public void ContainsTest()
    {
        var pair = testData.First();
        controller.Add(pair.Item1, pair.Item2);
        var actionResult = controller.Contains(pair.Item1, pair.Item2);

        var okResult = actionResult.Result as OkObjectResult;

        Assert.That(okResult, Is.Not.EqualTo(null));
        var value = okResult.Value as StudentExamResult;
        Assert.That(value != null);
        Assert.That(value.StudentId, Is.EqualTo(pair.Item1));
        Assert.That(value.CourseId, Is.EqualTo(pair.Item2));
        Assert.That(value.ExamResult, Is.EqualTo(StudentExamResult.Passed));
    }

    [Test]
    public void CountTest()
    {
        var pair = testData.First();

        controller.Add(pair.Item1, pair.Item2);
        var actionResult = controller.Count();

        var okResult = actionResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.EqualTo(null));
        var value = okResult.Value is int resultValue ? resultValue : 0;
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void RemoveTest()
    {
        var pair = testData.First();

        controller.Add(pair.Item1, pair.Item2);
        var removeResult = controller.Remove(pair.Item1, pair.Item2);
        var countResult = controller.Count();

        var okRemoveResult = removeResult.Result as OkObjectResult;
        Assert.That(okRemoveResult, Is.Not.EqualTo(null));
        var removeValue = okRemoveResult.Value as StudentStatusResult;
        Assert.That(removeValue != null);
        Assert.That(removeValue.StudentId, Is.EqualTo(pair.Item1));
        Assert.That(removeValue.CourseId, Is.EqualTo(pair.Item2));
        Assert.That(removeValue.StatusResult, Is.EqualTo(StudentStatusResult.Removed));

        var okCountResult = countResult.Result as OkObjectResult;
        Assert.That(okCountResult, Is.Not.EqualTo(null));
        var value = okCountResult.Value is int resultValue ? resultValue : -1;
        Assert.That(value, Is.Zero);
    }

    [Test]
    public void WorkingSessionTest()
    {
        foreach (var data in testData)
        {
            controller.Add(data.Item1, data.Item1);
        }
        var countResultAdd = controller.Count();

        foreach (var data in testData)
        {
            controller.Remove(data.Item1, data.Item1);
        }
        var countResultRemove = controller.Count();

        var okResultAdd = countResultAdd.Result as OkObjectResult;
        var okResultRemove = countResultRemove.Result as OkObjectResult;
        Assert.That(okResultAdd, Is.Not.EqualTo(null));
        Assert.That(okResultRemove, Is.Not.EqualTo(null));
        var valueAdd = okResultAdd.Value is int resultValueAdd ? resultValueAdd : 0;
        var valueRemove = okResultRemove.Value is int resultValueRemove ? resultValueRemove : 0;
        Assert.That(valueAdd, Is.EqualTo(testData.Count));
        Assert.That(valueRemove, Is.Zero);
    }
}