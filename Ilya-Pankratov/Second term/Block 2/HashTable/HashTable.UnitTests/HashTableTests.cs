using System.Collections.Generic;
using NUnit.Framework;

namespace HashTable.UnitTests
{
    public class HashTableTests
    {
        [Test]
        public void ConstructorTest()
        {
            var hashTable = new HashTable<int, int>();
            Assert.IsTrue(hashTable.ElementCount == 0);
            Assert.IsTrue(hashTable.KeyCount == 0);
        }

        [Test]
        public void AddAndContainsTest()
        {
            // first case: add <TKey: int, TValue: int>

            var firstHashTable = new HashTable<int, int>();
            var firstElement = new Element<int, int>(1, 2);
            firstHashTable.Add(firstElement);

            Assert.IsTrue(firstHashTable.Contains(firstElement));

            // second case: add <TKey: int, TValue: string>

            var secondHashTable = new HashTable<int, string>();
            var secondElement = new Element<int, string>(1, "MATMEX");
            secondHashTable.Add(secondElement);

            Assert.IsTrue(secondHashTable.Contains(secondElement));

            // third case: add <TKey: string, TValue: int>

            var thirdHashTable = new HashTable<string, int>();
            var thirdElement = new Element<string, int>("MATMEX", 1);
            thirdHashTable.Add(thirdElement);

            Assert.IsTrue(thirdHashTable.Contains(thirdElement));

            // fourth case: add <TKey: string, TValue: string>

            var fourthHashTable = new HashTable<string, string>();
            var fourthElement = new Element<string, string>("MATMEX", "TOP");
            fourthHashTable.Add(fourthElement);

            Assert.IsTrue(fourthHashTable.Contains(fourthElement));
        }

        [Test]
        public void AddRangeTest()
        {
            // add array of elements

            var hashTable = new HashTable<string, string>();
            var arrayElements = new Element<string, string>[3];

            for (var i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = new Element<string, string>(i.ToString(), "SPBU");
            }

            hashTable.AddRange(arrayElements);

            foreach (var element in arrayElements)
            {
                Assert.IsTrue(hashTable.Contains(element));
            }

            // add list of elements
            
            var listElements = new List<Element<string, string>>
            {
                new Element<string, string>("Student", "Ilya Pankratov"),
                new Element<string, string>("Teacher", "Sartasov Stanislav Yurievich")
            };

            hashTable.AddRange(listElements);

            foreach (var element in listElements)
            {
                Assert.IsTrue(hashTable.Contains(element));
            }
        }

        [Test]
        public void FindTest()
        {
            // preparation

            var hashTable = new HashTable<string, string>();
            var newElement = new Element<string, string>("Student", "Ilya Pankratov");
            hashTable.Add(newElement);

            // checking first case

            Element<string, string> foundElement = hashTable.Find(newElement);
            Assert.IsTrue(newElement.Key == foundElement.Key);
            Assert.IsTrue(newElement.Value == foundElement.Value);

            // checking second case

            Assert.IsNull(hashTable.Find(new Element<string, string>("Teacher", "Sartasov Stanislav Yurievich")));
        }

        [Test]
        public void RemoveTest()
        {
            // preparation

            var hashTable = new HashTable<string, string>();
           var newElement = new Element<string, string>("Student", "Ilya Pankratov");
            hashTable.Add(newElement);

            // checking first case

            hashTable.Remove(newElement);
            Assert.IsTrue(!hashTable.Contains(newElement));

            // checking second case

            Assert.IsFalse(hashTable.Remove(new Element<string, string>("Teacher", "Sartasov Stanislav Yurievich")));
        }

        [Test]
        public void IndexerTest()
        {
            // preparation
            
            var hashTable = new HashTable<string, string>();
            var newElement = new Element<string, string>("Student", "Ilya Pankratov");
            hashTable.Add(newElement);

            // checking first case

            List<Element<string, string>> elements = hashTable["Student"];
            Assert.IsTrue(elements[0].Key == newElement.Key && elements[0].Value == newElement.Value);

            // checking second case

            Assert.IsNull(hashTable["Teacher"]);
        }

        [Test]
        public void ClearTest()
        {
            // preparation

            var hashTable = new HashTable<string, string>();
            var elements = new List<Element<string, string>>
            {
                new Element<string, string>("Student", "Ilya Pankratov"),
                new Element<string, string>("Teacher", "Sartasov Stanislav Yurievich"),
                new Element<string, string>("Teacher", "Kirilenko Yakov Alexandrovich"),
                new Element<string, string>("Dean", "Alexander Igorevich Razov"),
                new Element<string, string>("University", "SPBU")
            };

            hashTable.AddRange(elements);

            // checking 

            hashTable.Clear();

            foreach (var element in elements)
            {
                Assert.IsFalse(hashTable.Contains(element));
            }
        }

        [Test]
        public void ContainsKeyTest()
        {
            // preparation

            var hashTable = new HashTable<string, string>();
            var element = new Element<string, string>("Student", "Ilya Pankratov");
            hashTable.Add(element);

            // checking 

            Assert.IsTrue(hashTable.ContainsKey("Student"));
            Assert.IsFalse(hashTable.ContainsKey("Teacher"));
        }

        [Test]
        public void ContainsValueTest()
        {
            // preparation

            var hashTable = new HashTable<string, string>();
            var firstElement = new Element<string, string>("Student", "Ilya Pankratov");
            var secondElement = new Element<string, string>("Teacher", "Sartasov Stanislav Yurievich");
            hashTable.Add(firstElement);
            hashTable.Add(secondElement);

            // checking 

            Assert.IsTrue(hashTable.ContainsValue("Sartasov Stanislav Yurievich"));
            Assert.IsFalse(hashTable.ContainsValue("SPBU"));
        }

        [Test]
        public void RebalanceTest()
        {
            var hashTable = new HashTable<string, int>();
            var elements = new List<Element<string, int>>();
            
            for (int i = 0; i < 10000; i++)
            {
                var element = new Element<string, int>(i.ToString(), i);
                elements.Add(element);
                hashTable.Add(element);
            }

            Assert.IsNotNull(hashTable);

            foreach (var element in elements)
            {
                Assert.IsTrue(hashTable.Contains(element));
            }
        }
    }
}