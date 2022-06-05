using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashTableLib;

namespace HashTableLibTests
{
	[TestClass]
	public class HashTableTests
	{
		public HashTable<string> Table = new HashTable<string>();
		
		[TestMethod]
		public void AddTest()
		{
			Table.Add(18, "19");
			Table.Add(18, "18");

			Assert.IsTrue(Table.Get(18).Equals("18"));
		}

		[TestMethod]
		public void WrongDelete()
		{            
			Assert.IsFalse(Table.Delete(222));
		}

		[TestMethod]
		public void WrongGet()
		{
			string get = Table.Get(222);
			Assert.IsTrue(get == null);
		}

		[TestMethod]
		public void RebalanceTest()
		{
			Table.Add(18, "18");
			Table.Add(21, "21");
			Assert.IsTrue(Table.IsInChain(0, 18) && Table.IsInChain(3, 21));
		}
	}
}
