using NUnit.Framework;

namespace HashTable.UnitTests
{
    public class ElementTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case: int and int test

            int intTestKey = 1;
            int intTestValue = 2;

            var firstElement = new Element<int, int>(intTestKey, intTestValue);
            Assert.IsTrue(firstElement.Key == intTestKey && firstElement.Value == intTestValue);

            // second case: int and string test

            string stringTestValue = "2";

            var secondElement = new Element<int, string>(intTestKey, stringTestValue);
            Assert.IsTrue(secondElement.Key == intTestKey && secondElement.Value.CompareTo(stringTestValue) == 0);

            // third case: int and string test

            string stringTestKey = "1";

            var thirdElement = new Element<string, int>(stringTestKey, intTestValue);
            Assert.IsTrue(thirdElement.Key.CompareTo(stringTestKey) == 0 && thirdElement.Value == intTestValue);

            // fourth case: string and string test

            var fourthElement = new Element<string, string>(stringTestKey, stringTestValue);
            Assert.IsTrue(fourthElement.Key.CompareTo(stringTestKey) == 0 && fourthElement.Value.CompareTo(stringTestValue) == 0);
        }
    }
}