using NUnit.Framework;

namespace DoublyLinkedList.UnitTests;

public class DoublyLinkedListTests
{
    [Test]
    public void AddCountTest()
    {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        Assert.AreEqual(1, list.Count);
    }

    [Test]
    public void AddAndGetValueTest()
    {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        Assert.AreEqual(10, list.Get(0));
    }

    [Test]
    public void AddFewValuesCountTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i);
        }
        Assert.AreEqual(10, list.Count);
    }

    [Test]
    public void AddAndGetFewValuesTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(i * i, list.Get(i));
        }
    }

    [Test]
    public void RemoveFromEmptyListTest()
    {
        var list = new DoublyLinkedList<int>();
        Assert.AreEqual(list.Remove(123), false);
    }

    [Test]
    public void RemoveCountTest()
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
    public void RemoveFewValuesTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i += 2)
        {
            list.Add(i * i);
        }
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                Assert.AreEqual(true, list.Remove(i * i));
            }
            else
            {
                Assert.AreEqual(false, list.Remove(i * i));
            }
        }
    }

    [Test]
    public void RemoveAtFromEmptyListTest()
    {
        var list = new DoublyLinkedList<int>();
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(0));
    }

    [Test]
    public void RemoveAtCountTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        list.RemoveAt(0);
        list.RemoveAt(1);
        list.RemoveAt(7);
        list.RemoveAt(6);
        Assert.AreEqual(6, list.Count);
    }

    [Test]
    public void RemoveAtSmallerIndexTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(-5));
    }

    [Test]
    public void RemoveAtGreaterIndexTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.RemoveAt(10));
    }

    [Test]
    public void IndexOfFromEmptyListTest()
    {
        var list = new DoublyLinkedList<int>();
        Assert.AreEqual(-1, list.IndexOf(0));
    }

    [Test]
    public void IndexOfFewValuesTest()
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
    public void IndexOfUnexistingItemTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        Assert.AreEqual(-1, list.IndexOf(50));
    }

    [Test]
    public void GetFromEmptyListTest()
    {
        var list = new DoublyLinkedList<int>();
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.Get(0));
    }

    [Test]
    public void GetFewValuesTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(i * i, list.Get(i));
        }
    }

    [Test]
    public void GetSmallerIndexTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.Get(-5));
    }

    [Test]
    public void GetGreaterIndexTest()
    {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(i * i);
        }
        Assert.Catch<System.ArgumentOutOfRangeException>(() => list.Get(10));
    }
}
