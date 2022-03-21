using NUnit.Framework;
using MyListLibraly;

namespace Task3.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FindTest()
        {
            MyList<int> firstTestList = new MyList<int>();
            MyList<int> secondTestList = new MyList<int>();
            for (int i = 0; i < 12; i += 3)
            {
                firstTestList.AddFirst(i);
                firstTestList.AddLast(i + 1);
                secondTestList.AddLast(i);
                secondTestList.AddFirst(i + 1);
            }

                
            for (int i = 0; i<12; i++)
            {
                if ((i - 2) % 3 == 0)
                {
                    if (firstTestList.Find(i) != null || secondTestList.Find(i) != null) 
                        Assert.Fail(i.ToString());
                }
                    
                else
                    if (firstTestList.Find(i) == null || secondTestList.Find(i) == null) Assert.Fail(i.ToString());
            }
        }

        [Test]
        public void DeleteTest()
        {
            MyList<int> testList = new MyList<int>();

            for (int i = 3; i <= 9; i +=2)
            {
                testList.AddFirst(i);
                testList.AddLast(i + 1);
            }

            for (int i = 12; i >= 7; i--)
            {
                if (i>10 && testList.Delete(i)) Assert.Fail("0");

                else if(i<=10)
                {
                    Node<int> sampleNode;
                    if (i % 2 == 1)
                    {
                        sampleNode = testList.Head;
                    }
                    else
                    {
                        sampleNode = testList.Tail;
                    }

                    if (!( testList.Length() == 8 + i - 10 && testList.Delete(i)))  Assert.Fail("1123");

                    if (i % 2 == 1)
                    {
                        if (testList.Head.Previous != null || sampleNode.Next != testList.Head)
                            Assert.Fail("2");
                    }
                    else
                    {
                        if (testList.Tail.Next != null)
                            Assert.Fail(i.ToString());
                    }
                    
                }
            }
            for (int i = 3; i <= 4; i++)
            {
                Node<int> deletable = testList.Find(i);
                if (!testList.Delete(i) || testList.Length() != 3 - (i - 3)) Assert.Fail();
                if ( deletable.Next.Previous != deletable.Previous ||
                    deletable.Previous.Next != deletable.Next) Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void AddFirstTest()
        {
            MyList<int> testList = new MyList<int>();

            for (int i = 0; i <= 10; i ++)
            {
                testList.AddFirst(i);
                if (testList.Length() != i + 1) Assert.Fail("1");
            }            
    
            Node<int> tmp = testList.Head;

            for (int i = 0; i < 10; i++)
            {
                if (tmp.Next.Item - tmp.Item != -1) Assert.Fail(i.ToString()+"as");
                tmp = tmp.Next;
            }
            Assert.Pass();
        }
        [Test]
        public void AddLastTest()
        {
            MyList<int> testList = new MyList<int>();

            for (int i = 0; i <= 10; i++)
            {
                testList.AddLast(i);
                if (testList.Length() != i + 1) Assert.Fail("1");
            }

            Node<int> tmp = testList.Tail;

            for (int i = 0; i < 10; i++)
            {
                if (tmp.Previous.Item - tmp.Item != -1) Assert.Fail(i.ToString() + "as");
                tmp = tmp.Previous;
            }
            Assert.Pass();
        }


        [Test]
        public void AddRange()
        {
            MyList<int> testList = new MyList<int>();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = i;

            testList.AddRange(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (testList.Find(i) == null) Assert.Fail();
            }
            Assert.Pass();
        }
    }
}