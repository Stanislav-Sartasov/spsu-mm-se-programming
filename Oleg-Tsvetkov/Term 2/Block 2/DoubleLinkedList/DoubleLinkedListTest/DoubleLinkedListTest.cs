using DoubleLinkedListLib;
using NUnit.Framework;

namespace DoubleLinkedListTest;

public class Tests
{
    private DoubleLinkedList<int> currentList;

    [SetUp]
    public void Setup()
    {
        currentList = new DoubleLinkedList<int>(0);
    }

    [Test]
    public void TestListFind()
    {
        Assert.IsTrue(currentList.Find(0));
        Assert.IsFalse(currentList.Find(10));
    }

    [Test]
    public void TestListAdd()
    {
        Assert.IsFalse(currentList.Find(10));
        currentList.Add(10);
        Assert.IsTrue(currentList.Find(10));
        currentList.Add(10);
        Assert.IsTrue(currentList.Find(10));
        currentList.Add(-10);
        currentList.Add(5);
        Assert.IsTrue(currentList.Find(-10));
        Assert.IsTrue(currentList.Find(5));
        currentList.Add(-9);
        currentList.Add(9);
        currentList.Add(11);
        currentList.Add(-11);
        currentList.Add(4);
        Assert.IsTrue(currentList.Find(-9));
        Assert.IsTrue(currentList.Find(9));
        Assert.IsTrue(currentList.Find(11));
        Assert.IsTrue(currentList.Find(-11));
        Assert.IsTrue(currentList.Find(4));
    }

    [Test]
    public void TestListRemove()
    {
        Assert.IsFalse(currentList.Find(10));
        currentList.Add(10);
        Assert.IsTrue(currentList.Find(10));
        currentList = currentList.Remove(10);
        Assert.IsFalse(currentList.Find(10));
        currentList.Add(10);
        currentList = currentList.Remove(0);
        Assert.IsFalse(currentList.Find(0));
    }

    [Test]
    public void TestIfRemoveReturningNull()
    {
        Assert.IsNull(currentList.Remove(0));
        currentList.Add(10);
        Assert.IsTrue(currentList.Find(10));
        currentList = currentList.Remove(10);
        Assert.IsFalse(currentList.Find(10));
    }

    [Test]
    public void TestToString()
    {
        Assert.IsTrue(currentList.ToString().Equals("0"));
        currentList.Add(10);
        Assert.IsTrue(currentList.ToString().Equals("0 10"));
        currentList = currentList.Next;
        Assert.IsTrue(currentList.ToString().Equals("0 10"));
        currentList = currentList.Remove(0);
        Assert.IsFalse(currentList.Find(0));
        Assert.IsTrue(currentList.ToString().Equals("10"));
    }
}