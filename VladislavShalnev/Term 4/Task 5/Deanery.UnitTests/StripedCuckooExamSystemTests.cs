using Deanery.ExamSystems;
using NUnit.Framework;

namespace Deanery.UnitTests;

public class StripedCuckooExamSystemTests
{
	private StripedCuckooExamSystem _examSystem;

	[SetUp]
	public void Setup()
	{
		_examSystem = new StripedCuckooExamSystem();
	}

	[Test]
	public void AddTest()
	{
		int count = 10;

		for (int i = 0; i < count; i++)
			_examSystem.Add(i, count - i);

		Assert.AreEqual(count, _examSystem.Count);

		for (int i = 0; i < count; i++)
			Assert.IsTrue(_examSystem.Contains(i, count - i));
	}

	[Test]
	public void RemoveTest()
	{
		int count = 10;

		for (int i = 0; i < count; i++)
			_examSystem.Add(i, count - i);

		Assert.IsTrue(_examSystem.Contains(4, 6));

		_examSystem.Remove(4, 6);

		Assert.IsFalse(_examSystem.Contains(4, 6));
	}

	[Test]
	public void CountTest()
	{
		int count = 10;

		for (int i = 0; i < count; i++)
			_examSystem.Add(i, count - i);

		Assert.AreEqual(count, _examSystem.Count);

		_examSystem.Remove(4, 6);

		Assert.AreEqual(count - 1, _examSystem.Count);
	}

	[Test]
	public void ResizeTest()
	{
		int count = 100;

		for (int i = 0; i < count; i++)
			_examSystem.Add(i, count - i);

		Assert.AreEqual(count, _examSystem.Count);
	}
}