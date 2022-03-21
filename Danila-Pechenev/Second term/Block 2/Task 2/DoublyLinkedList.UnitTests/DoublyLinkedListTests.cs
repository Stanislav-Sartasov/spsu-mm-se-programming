using NUnit.Framework;

namespace DoublyLinkedList.UnitTests;

public class DoublyLinkedListTests
{
    [Test]
    public void AddTest1()
    {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        Assert.AreEqual(1, list.Count);
    }

    [Test]
    public void AddTest2()
    {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        Assert.AreEqual(10, list.Get(0));
    }

    [Test]
    public void AddTest3()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        Assert.AreEqual(10, list.Count);
    }

    [Test]
    public void AddTest4()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(i, list.Get(i));
        }
    }

    [Test]
    public void RemoveTest1()
    {
        var list = new DoublyLinkedList<int>();
        Assert.AreEqual(list.Remove(123), false);
    }

    [Test]
    public void RemoveTest2()
    {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Remove(10);
        list.Remove(20);
        Assert.AreEqual(list.Count, 1);
    }

    [Test]
    public void RemoveTest3()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i += 2)
        {
            list.Add(i);
        }
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                Assert.AreEqual(true, list.Remove(i));
            }
            else
            {
                Assert.AreEqual(false, list.Remove(i));
            }
        }
    }

    [Test]
    public void RemoveAtTest1()
    {
        var list = new DoublyLinkedList<int>();
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(0));
    }

    [Test]
    public void RemoveAtTest2()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        list.RemoveAt(0);
        list.RemoveAt(1);
        list.RemoveAt(7);
        list.RemoveAt(6);
        Assert.AreEqual(6, list.Count);
    }

    [Test]
    public void RemoveAtTest3()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(-5));
    }

    [Test]
    public void RemoveAtTest4()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(10));
    }

    [Test]
    public void IndexOfTest1()
    {
        var list = new DoublyLinkedList<int>();
        Assert.AreEqual(-1, list.IndexOf(0));
    }

    [Test]
    public void IndexOfTest2()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(i, list.IndexOf(i * i));
        }
    }

    [Test]
    public void IndexOfTest3()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        Assert.AreEqual(-1, list.IndexOf(10));
    }
}
