using NUnit.Framework;
using MyListLibrary;

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

                
            for (int i = 0; i < 12; i++)
            {
                if ((i - 2) % 3 == 0)
                {
                    if (firstTestList.Find(i) != null || secondTestList.Find(i) != null) 
                        Assert.Fail();
                }
                    
                else
                    if (firstTestList.Find(i) == null || secondTestList.Find(i) == null) Assert.Fail();
            }
        }

        [Test]
        public void DeleteTest()
        {
            MyList<int> testList = new MyList<int>();

            for (int i = 3; i <= 9; i += 2)
            {
                testList.AddFirst(i);
                testList.AddLast(i + 1);
            }

            for (int i = 12; i >= 7; i--)
            {
                if (i > 10 && testList.Delete(i)) Assert.Fail();

                else if(i <= 10)
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

                    if (!( testList.Length() == 8 + i - 10 && testList.Delete(i)))  Assert.Fail();

                }
            }
            for (int i = 3; i <= 4; i++)
            {
                Node<int> deletable = testList.Find(i);
                if (!testList.Delete(i) || testList.Length() != 3 - (i - 3)) Assert.Fail();

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
                if (testList.Length() != i + 1) Assert.Fail();
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
                if (testList.Length() != i + 1) Assert.Fail();
            }

            Assert.Pass();
        }


        [Test]
        public void AddRange()
        {
            MyList<int> testList = new MyList<int>();
            int[] array = new int[10];
            for (int i = 0; i < array.Length; i++)
                array[i] = i;

            testList.AddRange(array);

            for (int i = 0; i < array.Length; i++)
            {
                if (testList.Find(i) == null) Assert.Fail();
            }
            Assert.Pass();
        }
    }
}