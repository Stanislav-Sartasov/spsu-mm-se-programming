using Task3;

namespace ArrayTests.Tests
{
    public class Tests
    {

        [Test]
        public void AddingTest()
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 5; i < 24; i++)
                TestArray.Add(i);
            Assert.That(8, Is.EqualTo(TestArray[3]));

            Assert.Pass();
        }


        [TestCase(14)]
        [TestCase(-10)]
        public void IndexExeptionTest(int index)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                TestArray.Add(i);
            var ex = Assert.Throws<IndexOutOfRangeException>(() => TestArray.GetItem(index));
            Assert.That(ex.Message, Is.EqualTo("Invalid index"));

            Assert.Pass();

        }


        [TestCase(2)]
        public void IndexIsCorrectTest(int index)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                TestArray.Add(i);
            var result = TestArray.GetItem(index);
            Assert.That(result, Is.EqualTo(2));

            Assert.Pass();

        }


        [TestCase(24)]
        public void GetIndexTest(int value)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 30; i++)
                TestArray.Add(i * 12);
            var result = TestArray.GetIndex(value);
            Assert.That(result, Is.EqualTo(2));

            Assert.Pass();

        }

        [TestCase(3)]
        public void ResizeIsWrongTest(int newSize)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 6; i++)
                TestArray.Add(i);

            var ex = Assert.Throws<Exception>(() => TestArray.Resize(newSize));
            Assert.That(ex.Message, Is.EqualTo("The new size is less than the allowed one"));

            Assert.Pass();
        }


        [TestCase(-1)]
        [TestCase(100)]
        public void RemovingByIndexExeptionTest(int index)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                TestArray.Add(i);
            var ex = Assert.Throws<IndexOutOfRangeException>(() => TestArray.RemoveAt(index));
            Assert.That(ex.Message, Is.EqualTo("Invalid index"));

            Assert.Pass();

        }

        [TestCase(312)]
        public void RemovingByIndexTest(int index)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 0; i <= 312; i++)
                TestArray.Add(i);
            TestArray.RemoveAt(index);
            Assert.That(TestArray.CountOfElements, Is.EqualTo(312));

            Assert.Pass();
        }

        [TestCase(24)]
        public void RemovingElementTest(int value)
        {
            var TestArray = new DynamicArray<int>();
            for (int i = 8; i <= 100; i *= 3)
                TestArray.Add(i);
            int firstValue = TestArray.GetIndex(value);
            TestArray.Remove(value);
            Assert.That(value, Is.Not.EqualTo(TestArray[firstValue]));

            Assert.Pass();

        }

    }
}