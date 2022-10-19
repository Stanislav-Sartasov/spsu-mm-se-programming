namespace Task3.Tests
{
    public class DynamicArrayTests
    {

        [Test]
        public void AddingTest()
        {
            var testArray = new DynamicArray<int>();
            for (int i = 5; i < 24; i++)
                testArray.Add(i);
            Assert.That(testArray[3], Is.EqualTo(8));

            Assert.Pass();
        }


        [TestCase(14)]
        [TestCase(-10)]
        public void IndexExeptionTest(int index)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                testArray.Add(i);
            var ex = Assert.Throws<IndexOutOfRangeException>(() => testArray.GetByIndex(index));
            Assert.That(ex.Message, Is.EqualTo("Invalid index"));

            Assert.Pass();
        }


        [TestCase(2)]
        public void IndexIsCorrectTest(int index)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                testArray.Add(i);
            var result = testArray.GetByIndex(index);
            Assert.That(result, Is.EqualTo(2));

            Assert.Pass();
        }



        [TestCase(24)]
        public void GetIndexTest(int value)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 0; i <= 30; i++)
                testArray.Add(i * 12);
            var result = testArray.GetIndex(value);
            Assert.That(result, Is.EqualTo(2));

            Assert.Pass();
        }

 

        [TestCase(-1)]
        [TestCase(100)]
        public void DeletingByIndexExeptionTest(int index)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 0; i <= 13; i++)
                testArray.Add(i);
            var ex = Assert.Throws<IndexOutOfRangeException>(() => testArray.DeleteByIndex(index));
            Assert.That(ex.Message, Is.EqualTo("Invalid index"));

            Assert.Pass();
        }

        [TestCase(312)]
        public void DeletingByIndexTest(int index)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 0; i <= 312; i++)
                testArray.Add(i);
            testArray.DeleteByIndex(index);
            Assert.That(testArray.CountOfElements, Is.EqualTo(312));

            Assert.Pass();
        }

        [TestCase(24)]
        public void DeletingElementTest(int value)
        {
            var testArray = new DynamicArray<int>();
            for (int i = 8; i <= 100; i *= 3)
                testArray.Add(i);
            int firstValue = testArray.GetIndex(value);
            testArray.DeleteElement(value);
            Assert.That(value, Is.Not.EqualTo(testArray[firstValue]));

            Assert.Pass();
        }

    }
}